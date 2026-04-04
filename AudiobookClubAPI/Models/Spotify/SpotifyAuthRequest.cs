namespace AudiobookClubAPI.Models.Spotify;

public class SpotifyAuthRequest
{
    public string Code { get; set; }
    public string RedirectUri { get; set; }
}