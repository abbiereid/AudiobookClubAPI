using AudiobookClubAPI.Models.Spotify;
using AudiobookClubAPI.Services;

namespace AudiobookClubAPI.Facades.Spotify;

public class SpotifyFacade : ISpotifyFacade
{
    private readonly ISpotifyClient _spotifyClient;
    
    public SpotifyFacade(ISpotifyClient spotifyClient)
    {
        _spotifyClient = spotifyClient;
    }
    public Task LoginToSpotify(SpotifyAuthRequest request)
    {
        _spotifyClient.Authenticate(request);
        return Task.CompletedTask;
    }
}