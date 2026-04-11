using AudiobookClubAPI.Models.Spotify;
using AudiobookClubAPI.Services;

namespace AudiobookClubAPI.Facades.Spotify;

public class SpotifyFacade(ISpotifyClient spotifyClient, IHttpContextAccessor httpContextAccessor, ISessionService sessionService) : ISpotifyFacade
{
    public async Task<string> LoginToSpotify(SpotifyAuthRequest request)
    {
        var response = await spotifyClient.ExchangeAuthCodeForAccessTokenAsync(request);
        return sessionService.CreateSession(response);
    }

    public async Task<SpotifyUser> GetCurrentSpotifyUserInfo()
    {
        var accessToken = httpContextAccessor.HttpContext?.Items["SpotifyAccessToken"] as string ?? throw new InvalidOperationException("Spotify access token not found. Please log into Spotify.");
        return await spotifyClient.GetCurrentSpotifyUserAsync(accessToken);
    }
}