using Microsoft.Extensions.Logging;
using MovieService.Data.Interfaces.Services;
using MovieService.Domain.Entities;
using MovieService.Service.Interfaces.Services;

namespace MovieService.Service.Services;

public class ServiceBase
{
    private readonly ILogger _logger;

    public ServiceBase(ILogger logger)
    {
        _logger = logger;
    }

    protected ILogger Logger => _logger;
}
public class MovieService : ServiceBase, IMovieService
{
    private readonly IMovieCrudService _movieCrudService;
    public MovieService(IMovieCrudService movieCrudService, ILogger<MovieService> logger) : base(logger)
    {
        _movieCrudService = movieCrudService;
    }
    public async Task<Movie> CreateMovie(Movie movie)
    {
        try
        {
            await _movieCrudService.CreateAsync(movie).ConfigureAwait(false);
            return movie;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Unable to CreateMovie");
            throw;
        }

    }
}
