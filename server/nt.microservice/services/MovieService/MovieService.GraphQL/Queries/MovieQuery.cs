using HotChocolate;
using MovieService.GraphQL.Types;
using MovieService.Service.Interfaces.Services;
using Omu.ValueInjecter;

namespace MovieService.GraphQL.Queries;

public class MovieQuery([Service]IMovieService movieService)
{
    [GraphQLDescription("Find Movie by partial name")]
    public async IAsyncEnumerable<MovieType> FindMovie([GraphQLName("searchTerm")]string searchTerm)
    {
        var movieResult = movieService.Search(searchTerm);

        await foreach (var movie in movieResult)
        {
            yield return Mapper.Map<MovieType>(movieResult);
        }
    }
}

//public class QueryType : ObjectType<Query>
//{
//    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
//    {
//        descriptor.Field(x => x.FindMovie(default!))
//                   .Type<ListType<MovieType>>()
//                   .Argument("searchTerm", x=> x.Type<StringType>())
//                   .Description("Find Movie by partial name");
//    }
//}
