using AudiobookClubAPI.Models.Spotify;

namespace AudiobookClubAPI.Facades.Spotify;

public interface ISpotifyFacade
{
    Task LoginToSpotify(SpotifyAuthRequest request);
}