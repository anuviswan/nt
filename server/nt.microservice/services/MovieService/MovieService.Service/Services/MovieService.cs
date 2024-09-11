using MovieService.Domain.Entities;
using MovieService.Service.Interfaces.Services;

namespace MovieService.Service.Services;

public class MovieService : IMovieService
{
    public Task<Movie> CreateMovie(Movie movie)
    {
        throw new NotImplementedException();
    }
}
