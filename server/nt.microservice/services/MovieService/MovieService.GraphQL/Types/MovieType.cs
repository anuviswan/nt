using HotChocolate.Types;
using MovieService.Service.Interfaces.Dtos;

namespace MovieService.GraphQL.Types
{
    public class MovieType : ObjectType<MovieDto>
    {
        protected override void Configure(IObjectTypeDescriptor<MovieDto> descriptor)
        {
            descriptor.Description("Defines a movie");

            descriptor.Field(x=>x.Title)
                      .Type<StringType>()
                      .Description("Title of Movie");
            descriptor.Field(x=>x.Language)
                      .Type<StringType>()
                      .Description("Language of movie");
            descriptor.Field(x=>x.ReleaseDate)
                      .Type<DateTimeType>()
                      .Description("Release date of movie");
        }
    }
}
