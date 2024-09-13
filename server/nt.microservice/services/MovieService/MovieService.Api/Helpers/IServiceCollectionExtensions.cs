namespace MovieService.Api.Helpers;

public static class IServiceCollectionExtensions
{
    public static void RegisterServices(this IServiceCollection serviceCollection)
    {
        Services.Register(serviceCollection);
    }
}
