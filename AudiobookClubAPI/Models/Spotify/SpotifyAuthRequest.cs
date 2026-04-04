namespace AudiobookClubAPI.Models.Spotify;

public class SpotifyAuthRequest
{
    public string Grant_type { get; set; }
    public string Code { get; set; }
    public string Redirect_uri { get; set; }
}