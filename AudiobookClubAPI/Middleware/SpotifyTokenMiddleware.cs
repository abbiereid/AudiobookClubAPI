using AudiobookClubAPI.Services;
namespace AudiobookClubAPI.Middleware;

public class SpotifyTokenMiddleware(RequestDelegate next, ISessionService sessionService, ISpotifyClient spotifyClient)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var sessionId = context.Request.Cookies["SessionId"];
        if (sessionId == null)
        {
            await next(context);
            return;
        }
        var sessionData = sessionService.GetSession(sessionId);
        if(sessionData == null)
        {
            await next(context);
            return;
        }
        if (sessionData.ExpiresAt > DateTime.UtcNow)
        {
            context.Items["SpotifyAccessToken"] = sessionData.AccessToken;
            await next(context);
            return;
        }

        var refreshResponse = await spotifyClient.RefreshAccessTokenAsync(sessionData.RefreshToken);
        sessionData.AccessToken = refreshResponse.AccessToken;
        sessionData.ExpiresAt = DateTime.UtcNow.AddSeconds(refreshResponse.ExpiresIn);
        sessionService.UpdateSession(sessionId, sessionData);
        context.Items["SpotifyAccessToken"] = sessionData.AccessToken;
        
        await next(context);
    }
}