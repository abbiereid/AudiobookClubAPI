using AudiobookClubAPI.Facades.Spotify;

namespace AudiobookClubAPI.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ISpotifyFacade, SpotifyFacade>();
        return services;
    }
}