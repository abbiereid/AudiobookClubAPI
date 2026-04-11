using AudiobookClubAPI.Facades.Spotify;
using AudiobookClubAPI.Models.Spotify;
using Microsoft.AspNetCore.Mvc;

namespace AudiobookClubAPI.Controllers.Spotify;

[ApiController]
[Route("[controller]")]
public class SpotifyController(ISpotifyFacade spotifyFacade) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> LoginToSpotify([FromBody] SpotifyAuthRequest request)
    {
        var sessionId = await spotifyFacade.LoginToSpotify(request);
        
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(60)
        };
        Response.Cookies.Append("SessionId", sessionId, cookieOptions);
        return Ok();
    }
    
    [HttpGet("me")]
    public async Task<IActionResult> GetSpotifyUserInfo()
    {
        if(HttpContext.Items["SpotifyAccessToken"] == null)
            return Unauthorized("Spotify access token is missing. Please log in to Spotify first.");
        
        var response = await spotifyFacade.GetCurrentSpotifyUserInfo();
        return Ok(response);
    }
}