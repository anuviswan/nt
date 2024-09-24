using MovieService.Data.Interfaces.Entities;
using MovieService.Service.Interfaces.Dtos;
using Omu.ValueInjecter;

namespace MovieService.Api.Helpers;

public static class ValueInjectorMapper
{
    public static void RegisterTypes()
    {
        Mapper.AddMap<PersonEntity, PersonDto>(src=> new PersonDto { Name = src.Name });

        Mapper.AddMap<MovieEntity, MovieDto>(src =>
        {
            var dto = new MovieDto().InjectFrom(src) as MovieDto;
            // Map the CastAndCrew dictionary
            if (src.CastAndCrew is {})
            {
                dto.CastAndCrew = src.CastAndCrew.ToDictionary(
                    kvp => kvp.Key,
                    kvp => new List<PersonDto> { Mapper.Map<PersonDto>(kvp.Value) }// Map each PersonEntity to PersonDto
                );
            }

            return dto;
        });
    }
}
