using MovieService.Service.Interfaces.Dtos;
using MovieService.Service.Interfaces.Services;
namespace MovieService.GraphQL.Queries;

public class Query
{
    private readonly IMovieService _movieService;
    public Query(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [UseFiltering]
    public IEnumerable<MovieDto> FindMovie(string searchTerm)
    {
        return _movieService.Search(searchTerm);
    }
}
