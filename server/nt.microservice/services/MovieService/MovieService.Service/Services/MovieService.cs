using Microsoft.Extensions.Logging;
using MovieService.Data.Interfaces.Entities;
using MovieService.Data.Interfaces.Services;
using MovieService.Service.Interfaces.Dtos;
using MovieService.Service.Interfaces.Services;
using Omu.ValueInjecter;
using System.Linq;
using System.Threading.Tasks;

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
            await _movieCrudService.CreateAsync(Mapper.Map<MovieEntity>(movie)).ConfigureAwait(false);
            return movie;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Unable to CreateMovie");
            throw;
        }

    }

    public async IAsyncEnumerable<MovieDto> GetRecentMovies(int count = 10)
    {
        if (count <= 0)
            yield break; 

        await foreach(var movie in _movieCrudService.GetRecentMovies(count))
        {
            yield return Mapper.Map<MovieDto>(movie);
        }

    }

    public async IAsyncEnumerable<MovieDto> Search(string searchTerm)
    {
        if(string.IsNullOrEmpty(searchTerm))
            yield break;

        await foreach (var movie in _movieCrudService.SearchAsync(searchTerm))
        {
            yield return Mapper.Map<MovieDto>(movie);
        }
    }
}
