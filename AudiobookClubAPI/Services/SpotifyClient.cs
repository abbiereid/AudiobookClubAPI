using System.Net.Http.Headers;
using System.Text;
using AudiobookClubAPI.Models.Spotify;

namespace AudiobookClubAPI.Services;

public class SpotifyClient : ISpotifyClient
{
    private readonly SpotifyAPI.Web.SpotifyClient _client;
    private readonly HttpClient _httpClient;

    public SpotifyClient(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        
        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{configuration["Spotify:ClientId"]}:{configuration["Spotify:ClientSecret"]}"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
    }
    public async Task Authenticate(SpotifyAuthRequest request)
    {
        var response = await _httpClient.PostAsync("https://accounts.spotify.com/api/token", new FormUrlEncodedContent(new Dictionary<string, string>
        {
            {"grant_type", request.Grant_type},
            {"code", request.Code},
            {"redirect_uri", request.Redirect_uri},
        }));
        
        Console.WriteLine(response);
    }
}
