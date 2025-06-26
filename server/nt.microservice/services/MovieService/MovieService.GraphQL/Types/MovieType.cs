using HotChocolate;
using HotChocolate.Types;
using MovieService.Service.Interfaces.Dtos;

namespace MovieService.GraphQL.Types;

public class MovieType //: ObjectType<MovieDto>
{
    [GraphQLName("title")]
    [GraphQLDescription("Title of movie.")]
    public string Title { get; set; } = null!;

    [GraphQLName("description")]
    [GraphQLDescription("Synopsis of the movie")]
    public string Synopsis { get; set; } = null!;

    [GraphQLName("movieLanguage")]
    [GraphQLDescription("Language")]
    public string MovieLanguage { get; set; } = null!;


    [GraphQLName("releaseDate")]
    [GraphQLDescription("Release Date")]
    public DateTime ReleaseDate { get; set; }


    [GraphQLName("cast")]
    [GraphQLDescription("Cast of the movie")]
    public List<PersonType> Cast { get; set; } = new List<PersonType>();
}

public class PersonType //: ObjectType<PersonDto>
{
    [GraphQLName("name")]
    [GraphQLDescription("Name")]
    public string Name { get; set; } = null!;
}
