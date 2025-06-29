﻿using MovieService.GraphQL.Queries;

namespace MovieService.Api.Helpers;

public static class GraphQL
{
    public static void Register(IServiceCollection serviceCollection)
    {
        serviceCollection.AddGraphQLServer()
                         .AddQueryType<MovieQuery>()
                         .AddFiltering();
    }
}
