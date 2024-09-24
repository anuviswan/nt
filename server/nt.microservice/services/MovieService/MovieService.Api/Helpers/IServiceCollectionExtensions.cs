using MovieService.GraphQL.Queries;
using MovieService.GraphQL.Types;

namespace MovieService.Api.Helpers;

public static class IServiceCollectionExtensions
{
    public static void RegisterServices(this IServiceCollection serviceCollection)
    {
        Services.Register(serviceCollection);
    }

    public static void RegisterGraphQl(this IServiceCollection serviceCollection)
    {
        GraphQL.Register(serviceCollection);
    }
}
