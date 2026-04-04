using System.Net.Http.Headers;
using System.Text;
using AudiobookClubAPI.Models.Spotify;

namespace AudiobookClubAPI.Services;

public class SpotifyClient : ISpotifyClient
{
    private readonly HttpClient _authHttpClient;
    private readonly HttpClient _httpClient;

    public SpotifyClient(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _authHttpClient = new HttpClient();
        
        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{configuration["Spotify:ClientId"]}:{configuration["Spotify:ClientSecret"]}"));
        _authHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
    }
    public async Task<SpotifyTokenResponse> ExchangeAuthCodeForAccessTokenAsync(SpotifyAuthRequest request)
    {
        var response = await _authHttpClient.PostAsync("https://accounts.spotify.com/api/token", new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "grant_type", "authorization_code" },
                    { "code", request.Code },
                    { "redirect_uri", request.RedirectUri },
                }));
        
        return await ProcessTokenResponse(response);
    }

    public async Task<SpotifyTokenResponse> RefreshAccessTokenAsync(string refreshToken)
    {
        var response = await _authHttpClient.PostAsync("https://accounts.spotify.com/api/token", new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "grant_type", "refresh_token" },
                    { "refresh_token", refreshToken },
                }));
        
        return await ProcessTokenResponse(response);
    }

    private static async Task<SpotifyTokenResponse> ProcessTokenResponse(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<SpotifyTokenResponse>() ??
                   throw new InvalidOperationException();
        
        var exception = await response.Content.ReadAsStringAsync();
        throw new InvalidOperationException($"Failed Spotify Authentication with Status Code {response.StatusCode}: {exception}");
    }

    public async Task<SpotifyUser> GetCurrentSpotifyUserAsync(string accessToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "https://api.spotify.com/v1/me");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        
        var response = await _httpClient.SendAsync(request);
        
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<SpotifyUser>() ?? throw new InvalidOperationException();
        
        var exception = await response.Content.ReadAsStringAsync();
        throw new InvalidOperationException($"Failed to retrieve Spotify user info with Status Code {response.StatusCode}: {exception}");
    }
}
