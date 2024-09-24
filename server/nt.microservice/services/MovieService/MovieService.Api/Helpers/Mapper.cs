using MovieService.Data.Interfaces.Entities;
using MovieService.Service.Interfaces.Dtos;
using Omu.ValueInjecter;

namespace MovieService.Api.Helpers;

public static class ValueInjectorMapper
{
    public static void RegisterTypes()
    {
        // Define the mapping for PersonEntity to PersonDto
        Mapper.AddMap<PersonEntity, PersonDto>(src => new PersonDto { Name = src.Name });

        // Define the mapping for MovieEntity to MovieDto
        Mapper.AddMap<MovieEntity, MovieDto>(src =>
        {
            var dto = new MovieDto();
            // Use InjectFrom to copy the base properties
            dto.InjectFrom(src);

            // Map the CastAndCrew dictionary
            if (src.CastAndCrew != null && src.CastAndCrew.Any())
            {
                dto.CastAndCrew = src.CastAndCrew.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Select(personEntity => Mapper.Map<PersonDto>(personEntity)).ToList() // Map each PersonEntity to PersonDto
                );
            }

            return dto;
        });
    }
}
