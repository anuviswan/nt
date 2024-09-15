using Microsoft.Extensions.Options;
using MovieService.Api.Loggers;
using MovieService.Api.Settings;
using MovieService.Data.Interfaces.Services;
using MovieService.Data.Services;
using MovieService.Service.Interfaces.Services;

namespace MovieService.Api.Helpers;

public static class Services
{
    public static void Register(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IMovieService, Service.Services.MovieService>();
        serviceCollection.AddSingleton<IMovieCrudService, MovieCrudService>();

        RegisterInitializersAndProviders(serviceCollection);
    }

    private static void RegisterInitializersAndProviders(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<DatabaseInitializer>();
        serviceCollection.AddSingleton<ModuleInitializer>();
        serviceCollection.AddSingleton<ILoggerProvider>(serviceProvider =>
        {
            var loggerConfig = serviceProvider.GetRequiredService<IOptions<LoggerConfiguration>>();
            return new CustomLoggerProvider(loggerConfig);
        });
    }
}