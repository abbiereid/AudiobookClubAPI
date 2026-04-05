using AudiobookClubAPI.Facades.Spotify;
using AudiobookClubAPI.Services;
using Microsoft.Extensions.Caching.Memory;

namespace AudiobookClubAPI.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ISpotifyFacade, SpotifyFacade>();
        services.AddScoped<ISpotifyClient, SpotifyClient>();
        services.AddMemoryCache();
        return services;
    }
}