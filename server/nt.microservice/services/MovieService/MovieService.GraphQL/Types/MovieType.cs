using HotChocolate.Types;
using MovieService.Service.Interfaces.Dtos;

namespace MovieService.GraphQL.Types
{
    public class MovieType : ObjectType<MovieDto>
    {
        protected override void Configure(IObjectTypeDescriptor<MovieDto> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(x=>x.Title).Type<StringType>();
            descriptor.Field(x=>x.Language).Type<StringType>();
            descriptor.Field(x=>x.ReleaseDate).Type<DateTimeType>();
        }
    }
}
