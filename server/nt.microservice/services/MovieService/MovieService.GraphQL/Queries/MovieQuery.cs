using HotChocolate;
using MovieService.GraphQL.Types;
using MovieService.Service.Interfaces.Services;

namespace MovieService.GraphQL.Queries;

public class MovieQuery([Service]IMovieService movieService)
{
    [GraphQLDescription("Find Movie by partial name")]
    public async IAsyncEnumerable<MovieType> FindMovie([GraphQLName("searchTerm")]string searchTerm)
    {
        var movieResult = movieService.Search(searchTerm);

        await foreach (var dto in movieResult)
        {
            yield return new MovieType
            {
                Title = dto.Title,
                MovieLanguage = dto.MovieLanguage ?? "Unknown",
                ReleaseDate = dto.ReleaseDate ?? DateTime.MinValue,
                Synopsis = "Synopsis not provided", // Assuming no synopsis in DTO
                Cast = dto.Cast?.Select(x=>new PersonType {  Name = x.Name}).ToList() ?? [],
                Crew = dto.Crew?.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Select(p => new PersonType { Name = p.Name }).ToList()) ?? []
            };
        }
    }

    [GraphQLDescription("Find recently released movies")]
    public async IAsyncEnumerable<MovieType> GetRecentMovies([GraphQLName("count")]int count = 10)
    {
        var movieResult = movieService.GetRecentMovies(count);
        await foreach (var dto in movieResult)
        {
            yield return new MovieType
            {
                Title = dto.Title,
                MovieLanguage = dto.MovieLanguage ?? "Unknown",
                ReleaseDate = dto.ReleaseDate ?? DateTime.MinValue,
                Synopsis = "Synopsis not provided", // Assuming no synopsis in DTO
                Cast = dto.Cast?.Select(x=>new PersonType {  Name = x.Name}).ToList() ?? [],
                Crew = dto.Crew?.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Select(p => new PersonType { Name = p.Name }).ToList()) ?? []
            };
        }
    }
}



