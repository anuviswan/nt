using HotChocolate;
using HotChocolate.Types;
using MovieService.GraphQL.Types;
using MovieService.Service.Interfaces.Dtos;
using MovieService.Service.Interfaces.Services;

namespace MovieService.GraphQL.Queries;

public class Query
{
    public IEnumerable<MovieDto> FindMovie([Service]IMovieService movieService, string searchTerm)
    {
        return movieService.Search(searchTerm);
    }
}

public class QueryType : ObjectType<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor.Field(x => x.FindMovie(default!, default!))
                   .Type<ListType<MovieType>>()
                   .Argument("searchTerm", x=> x.Type<StringType>())
                   .Description("Find Movie by partial name");
    }
}
