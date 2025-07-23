using Microsoft.Extensions.Options;
using ReviewService.Application.Interfaces.Operations;
using ReviewService.Application.Interfaces.Services;
using ReviewService.Presenation.Api.Options;
using StackExchange.Redis;

namespace ReviewService.Presenation.Api.Helpers;

public static class IServiceCollectionExtension
{
    public static void RegisterServices(this IServiceCollection serviceCollection)
    {
        // Register your services here
        // Example: serviceCollection.AddSingleton<IReviewService, ReviewService>();
        serviceCollection.AddScoped<IReviewService, ReviewService.Application.Services.Operations.ReviewService>();

        // Generic Services
        serviceCollection.AddScoped<ICachingService, ReviewService.Application.Services.Services.CachingService>(sp =>
        {
            var connectionMultiplexer = sp.GetRequiredService<IConnectionMultiplexer>();
            var cacheOptions = sp.GetRequiredService<IOptions<CacheOptions>>().Value;
            return new ReviewService.Application.Services.Services.CachingService(connectionMultiplexer, cacheOptions.ExpirationInMinutes);
        });

        // Register initializers and providers
        RegisterInitializersAndProviders(serviceCollection);
    }
    private static void RegisterInitializersAndProviders(IServiceCollection serviceCollection)
    {
        // Add any initializers or providers needed for the application
        // Example: serviceCollection.AddSingleton<DatabaseInitializer>();
        serviceCollection.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var cacheOptions = sp.GetRequiredService<IOptions<CacheOptions>>().Value;
            if (cacheOptions.ConnectionString is null)
            {
                throw new InvalidOperationException("Redis connection string is not configured.");
            }
            return ConnectionMultiplexer.Connect(cacheOptions.ConnectionString);
        });

        serviceCollection.AddSingleton<DatabaseInitializer>();
        serviceCollection.AddSingleton<ModuleInitializer,ModuleInitializer>();
    }
}
