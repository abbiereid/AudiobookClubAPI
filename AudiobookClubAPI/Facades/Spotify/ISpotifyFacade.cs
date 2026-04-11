using AudiobookClubAPI.Models.Spotify;

namespace AudiobookClubAPI.Facades.Spotify;

public interface ISpotifyFacade
{
    Task<string> LoginToSpotify(SpotifyAuthRequest request);
    Task<SpotifyUser> GetCurrentSpotifyUserInfo();
}