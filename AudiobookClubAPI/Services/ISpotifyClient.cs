using AudiobookClubAPI.Models.Spotify;

namespace AudiobookClubAPI.Services;

public interface ISpotifyClient
{
    Task<SpotifyTokenResponse> ExchangeAuthCodeForAccessTokenAsync(SpotifyAuthRequest request);
    Task<SpotifyTokenResponse> RefreshAccessTokenAsync(string refreshToken);
    Task<SpotifyUser> GetCurrentSpotifyUserAsync(string accessToken);
}