using Microsoft.Extensions.Logging;
using MovieService.Data.Interfaces.Entities;
using MovieService.Data.Interfaces.Services;
using MovieService.Service.Interfaces.Dtos;
using MovieService.Service.Interfaces.Services;
using Omu.ValueInjecter;
using System.Runtime.InteropServices;

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
    public async Task<MovieDto> CreateMovie(MovieDto movie)
    {
        try
        {
            await _movieCrudService.CreateAsync(Mapper.Map<Movie>(movie)).ConfigureAwait(false);
            return movie;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Unable to CreateMovie");
            throw;
        }

    }

    public async IAsyncEnumerable<MovieDto> Search(string searchTerm)
    {
        var movieSearch = _movieCrudService.Search(searchTerm);
        await foreach (var movie in movieSearch)
        {
            yield return Mapper.Map<MovieDto>(movie);
        }
    }
}
