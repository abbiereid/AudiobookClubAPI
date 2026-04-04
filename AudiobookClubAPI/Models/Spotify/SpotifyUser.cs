using System.Text.Json.Serialization;

namespace AudiobookClubAPI.Models.Spotify;

public class SpotifyUser
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    
    [JsonPropertyName("display_name")]
    public string? DisplayName { get; set; }
    
    [JsonPropertyName("images")]
    public List<SpotifyUserProfileImage>? Images { get; set; }
}

public class SpotifyUserProfileImage
{
    [JsonPropertyName("url")]
    public string Url { get; set; }
}