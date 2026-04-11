using AudiobookClubAPI.Models.Spotify;
using Microsoft.Extensions.Caching.Memory;

namespace AudiobookClubAPI.Services;

public class SessionService(IMemoryCache cache) : ISessionService
{
    public string CreateSession(SpotifyTokenResponse tokenResponse)
    {
        if(tokenResponse.RefreshToken == null)
            throw new InvalidOperationException("Spotify did not return a refresh token. This is required for long-term access.");
        
        var session = new SessionData
        {
            AccessToken = tokenResponse.AccessToken,
            RefreshToken = tokenResponse.RefreshToken,
            ExpiresAt = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn)
        };
        
        var sessionId = Guid.NewGuid().ToString();
        UpdateSession(sessionId, session);
        return sessionId;
    }

    public SessionData? GetSession(string sessionId)
    {
        return cache.Get<SessionData>(sessionId);
    }

    public void UpdateSession(string sessionId, SessionData sessionData)
    {
        var slidingExpiration = new MemoryCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromDays(60)
        };
        cache.Set(sessionId, sessionData, slidingExpiration);
    }
}