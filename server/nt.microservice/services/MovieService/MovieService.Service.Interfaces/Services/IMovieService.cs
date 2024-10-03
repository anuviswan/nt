using MovieService.Service.Interfaces.Dtos;

namespace MovieService.Service.Interfaces.Services;

public interface IMovieService
{
    Task<MovieDto> CreateMovie(MovieDto movie);
    IEnumerable<MovieDto> Search(string searchTerm);
}
