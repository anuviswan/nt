using HotChocolate.Types;
using MovieService.Service.Interfaces.Dtos;

namespace MovieService.GraphQL.Types;

public class MovieType : ObjectType<MovieDto>
{
    protected override void Configure(IObjectTypeDescriptor<MovieDto> descriptor)
    {
        descriptor.Description("Defines a movie");

        descriptor.Field(x=>x.Title)
                  .Description("Title of Movie");
        descriptor.Field(x=>x.Language)
                  .Type<StringType>()
                  .Description("Language of movie");
        descriptor.Field(x=>x.ReleaseDate)
                  .Type<DateTimeType>()
                  .Description("Release date of movie");
        descriptor.Field(x => x.Cast)
                  .Type<ListType<PersonType>>()
                  .Description("Cast of the movie");
    }
}

public class PersonType : ObjectType<PersonDto>
{
    protected override void Configure(IObjectTypeDescriptor<PersonDto> descriptor)
    {
        descriptor.Field(x => x.Name)
            .Type<NonNullType<StringType>>()
            .Description("Name of the person");
    }
}
