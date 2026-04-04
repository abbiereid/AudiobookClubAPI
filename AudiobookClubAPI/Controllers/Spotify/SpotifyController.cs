using AudiobookClubAPI.Facades.Spotify;
using AudiobookClubAPI.Models.Spotify;
using Microsoft.AspNetCore.Mvc;

namespace AudiobookClubAPI.Controllers.Spotify;

[ApiController]
[Route("[controller]")]
public class SpotifyController : ControllerBase
{
    protected readonly ISpotifyFacade _spotifyFacade;
    
    public SpotifyController(ISpotifyFacade spotifyFacade)
    {
        _spotifyFacade = spotifyFacade;
    }

    [HttpPost("login")]
    public IActionResult LoginToSpotify([FromBody] SpotifyAuthRequest request)
    {
        _spotifyFacade.LoginToSpotify(request);
        return Ok();
    }
}