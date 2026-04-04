using AudiobookClubAPI.Models.Spotify;
using AudiobookClubAPI.Services;
using Microsoft.Extensions.Caching.Memory;

namespace AudiobookClubAPI.Facades.Spotify;

public class SpotifyFacade(ISpotifyClient spotifyClient, IMemoryCache cache) : ISpotifyFacade
{
    public async Task<string> LoginToSpotify(SpotifyAuthRequest request)
    {
        var response = await spotifyClient.ExchangeAuthCodeForAccessTokenAsync(request);
        
        if(response.RefreshToken == null)
            throw new InvalidOperationException("Spotify did not return a refresh token. This is required for long-term access.");

        var session = new SessionData
        {
            AccessToken = response.AccessToken,
            RefreshToken = response.RefreshToken,
            ExpiresAt = DateTime.UtcNow.AddSeconds(response.ExpiresIn)
        };
        
        var sessionId = Guid.NewGuid().ToString();
        UpdateSessionData(session, sessionId);
        
        return sessionId;
    }

    public async Task<SpotifyUser> GetCurrentSpotifyUserInfo(string sessionId)
    {
        var sessionData = cache.Get<SessionData>(sessionId) ?? throw new InvalidOperationException("Session not found or expired. Please log into Spotify.");

        if (sessionData.ExpiresAt > DateTime.UtcNow)
            return await spotifyClient.GetCurrentSpotifyUserAsync(sessionData.AccessToken);
        
        var refreshResponse = await spotifyClient.RefreshAccessTokenAsync(sessionData.RefreshToken);
        sessionData.AccessToken = refreshResponse.AccessToken;
        sessionData.ExpiresAt = DateTime.UtcNow.AddSeconds(refreshResponse.ExpiresIn);
        UpdateSessionData(sessionData, sessionId);

        return await spotifyClient.GetCurrentSpotifyUserAsync(sessionData.AccessToken);
    }

    private void UpdateSessionData(SessionData sessionData, string sessionId)
    {
        var slidingExpiration = new MemoryCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromDays(60)
        };
        
        cache.Set(sessionId, sessionData, slidingExpiration);
    }
}