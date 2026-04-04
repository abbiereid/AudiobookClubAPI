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
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(60)
        };
        Response.Cookies.Append("SessionId", sessionId, cookieOptions);
        return Ok();
    }
    
    [HttpGet("me")]
    public async Task<IActionResult> GetSpotifyUserInfo()
    {
        var sessionId = Request.Cookies["SessionId"] ?? throw new InvalidOperationException("SessionId cookie not found. Please log into Spotify.");
        var response = await spotifyFacade.GetCurrentSpotifyUserInfo(sessionId);
        return Ok(response);
    }
}