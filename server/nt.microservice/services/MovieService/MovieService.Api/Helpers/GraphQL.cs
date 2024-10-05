using MovieService.GraphQL.Queries;
using MovieService.GraphQL.Types;

namespace MovieService.Api.Helpers;

public static class GraphQL
{
    public static void Register(IServiceCollection serviceCollection)
    {
        serviceCollection.AddGraphQLServer()
                         .AddQueryType<QueryType>()
                         .AddFiltering();
    }
}
