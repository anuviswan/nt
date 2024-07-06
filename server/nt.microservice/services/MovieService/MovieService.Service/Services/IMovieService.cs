using MovieService.Domain.Entities;

namespace MovieService.Service.Services;

public interface IMovieService
{
    Task<Movie> CreateMovie(Movie movie);
}

public class MovieService : IMovieService
{
    public Task<Movie> CreateMovie(Movie movie)
    {
        throw new NotImplementedException();
    }
}
