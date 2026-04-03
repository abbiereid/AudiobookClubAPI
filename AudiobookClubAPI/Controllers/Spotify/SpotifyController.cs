using AudiobookClubAPI.Facades.Spotify;
using Microsoft.AspNetCore.Mvc;

namespace AudiobookClubAPI.Controllers.Spotify;

public class SpotifyController
{
    protected readonly string baseRoute = "spotify";
    protected readonly ISpotifyFacade _spotifyFacade;
    
    SpotifyController(ISpotifyFacade spotifyFacade)
    {
        _spotifyFacade = spotifyFacade;
    }

    [Route(("login"))]
    public Task LoginToSpotify()
    {
        Console.WriteLine("Login to Spotify");
        return null;
    }
}