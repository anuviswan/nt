
using Microsoft.AspNetCore.Mvc;
using MovieService.Api.ViewModels;
using MovieService.Domain.Entities;
using MovieService.Service.Services;
using Omu.ValueInjecter;

namespace MovieService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;
    private readonly ILogger<MovieController> _logger;
    public MovieController(IMovieService movieService,ILogger<MovieController> logger)
    {
        (_movieService, _logger) = (movieService, logger); 
    }

    public async Task<ActionResult<CreateMovieResponse>> CreateMovie(CreateMovieRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movieEntity = Mapper.Map<Movie>(request);
            var response = await _movieService.CreateMovie(movieEntity).ConfigureAwait(false);
            return Ok(Mapper.Map<CreateMovieResponse>(response));
        }
        catch (Exception)
        {

            throw;
        }
    } 
}