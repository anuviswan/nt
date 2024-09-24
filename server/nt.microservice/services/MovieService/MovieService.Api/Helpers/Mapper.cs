using MovieService.Data.Interfaces.Entities;
using MovieService.Service.Interfaces.Dtos;
using Omu.ValueInjecter;

namespace MovieService.Api.Helpers;

public static class ValueInjectorMapper
{
    public static void RegisterTypes()
    {
        Mapper.AddMap<MovieEntity, MovieDto>(src =>
        {
            var dto = new MovieDto().InjectFrom(src) as MovieDto;

            if (src.Director is { })
            {
                dto.Director = new PersonDto().InjectFrom(src.Director) as PersonDto;
            }

            return dto;
        });
    }
}
