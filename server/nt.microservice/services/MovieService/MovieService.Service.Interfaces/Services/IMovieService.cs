using MovieService.Service.Interfaces.Dtos;

namespace MovieService.Service.Interfaces.Services;

public interface IMovieService
{
    Task<MovieDto> CreateMovie(MovieDto movie);
    IAsyncEnumerable<MovieDto> Search(string searchTerm);
    IAsyncEnumerable<MovieDto> GetRecentMovies(int count = 10);

    Task<MovieDto?> GetMovieById(string id);
}
