using ReviewService.Application.Interfaces.Operations;

namespace ReviewService.Presenation.Api.Helpers;

public static class IServiceCollectionExtension
{
    public static void RegisterServices(this IServiceCollection serviceCollection)
    {
        // Register your services here
        // Example: serviceCollection.AddSingleton<IReviewService, ReviewService>();
        
        // Register initializers and providers
        RegisterInitializersAndProviders(serviceCollection);
    }
    private static void RegisterInitializersAndProviders(IServiceCollection serviceCollection)
    {
        // Add any initializers or providers needed for the application
        // Example: serviceCollection.AddSingleton<DatabaseInitializer>();

        serviceCollection.AddSingleton<DatabaseInitializer>();
        serviceCollection.AddSingleton<ModuleInitializer,ModuleInitializer>();
    }
}
