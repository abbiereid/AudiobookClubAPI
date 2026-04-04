using System.Text.Json.Serialization;

namespace AudiobookClubAPI.Models.Spotify;

public class SpotifyUser
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    
    [JsonPropertyName("display_name")]
    public string? DisplayName { get; set; }
    
    [JsonPropertyName("images")]
    public List<SpotifyUserProfileImages>? Images { get; set; }
}

public class SpotifyUserProfileImages
{
    [JsonPropertyName("url")]
    public string Url { get; set; }
}