using AudiobookClubAPI.Models.Spotify;

namespace AudiobookClubAPI.Services;

public interface ISessionService
{
    string CreateSession(SpotifyTokenResponse tokenResponse);
    SessionData? GetSession(string sessionId);
    void UpdateSession(string sessionId, SessionData sessionData);
}