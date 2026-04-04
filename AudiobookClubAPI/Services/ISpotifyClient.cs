using AudiobookClubAPI.Models.Spotify;

namespace AudiobookClubAPI.Services;

public interface ISpotifyClient
{
    Task Authenticate(SpotifyAuthRequest request);
}