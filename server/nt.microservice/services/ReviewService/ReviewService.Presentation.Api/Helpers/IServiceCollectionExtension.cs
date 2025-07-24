using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReviewService.Application.Interfaces.Operations;
using ReviewService.Application.Interfaces.Services;
using ReviewService.Domain.Entities;
using ReviewService.Presenation.Api.Options;
using StackExchange.Redis;

namespace ReviewService.Presenation.Api.Helpers;

public static class IServiceCollectionExtension
{
    public static void RegisterServices(this IServiceCollection serviceCollection)
    {
        

        // Register your services here
        // Example: serviceCollection.AddSingleton<IReviewService, ReviewService>();
        serviceCollection.AddAutoMapper(typeof(IServiceCollectionExtension));
        serviceCollection.AddMediatR(typeof(ReviewService.Application.Orchestration.Commands.CreateReviewCommand).Assembly);

        serviceCollection.AddSingleton<IMongoClient>(sp =>
        {
            var dbOptions = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            var connectionString = dbOptions.ConnectionString;
            return new MongoClient(connectionString);
        });

        serviceCollection.AddSingleton<IMongoDatabase>(sp =>
        {
            var dbOptions = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            var client = sp.GetRequiredService<IMongoClient>();
            var databaseName = dbOptions.DatabaseName;
            return client.GetDatabase(databaseName);
        });

        
        RegisterRepositories(serviceCollection);
        // User Services
        serviceCollection.AddScoped<IReviewService, ReviewService.Application.Services.Operations.ReviewService>();

        // Generic Services
        serviceCollection.AddSingleton<ICachingService, ReviewService.Application.Services.Services.CachingService>(sp =>
        {
            var connectionMultiplexer = sp.GetRequiredService<IConnectionMultiplexer>();
            var cacheOptions = sp.GetRequiredService<IOptions<CacheOptions>>().Value;
            return new ReviewService.Application.Services.Services.CachingService(connectionMultiplexer, cacheOptions.ExpirationInMinutes);
        });

        serviceCollection.AddSingleton<IReviewCachingService, ReviewService.Application.Services.Services.ReviewCachingService>();

        // Register initializers and providers
        RegisterInitializersAndProviders(serviceCollection);
    }

    private static void RegisterRepositories(IServiceCollection serviceCollection)
    {
        // Register your repositories here
        // Example: serviceCollection.AddScoped<IReviewRepository, ReviewRepository>();
        serviceCollection.AddScoped<Domain.Repositories.IReviewRepository, ReviewService.Infrastructure.Repository.Repositories.ReviewRepository>();
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

            var options = ConfigurationOptions.Parse(cacheOptions.ConnectionString);
            options.AbortOnConnectFail = false;
            options.ConnectRetry = 5;
            options.ConnectTimeout = 10000;
            return ConnectionMultiplexer.Connect(options);
        });

        serviceCollection.AddSingleton<DatabaseInitializer>();
        serviceCollection.AddSingleton<ModuleInitializer,ModuleInitializer>();
    }
}
