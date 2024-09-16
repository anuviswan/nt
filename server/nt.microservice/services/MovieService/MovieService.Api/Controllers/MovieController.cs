
using Microsoft.AspNetCore.Mvc;
using MovieService.Api.ViewModels;
using MovieService.Service.Interfaces.Dtos;
using MovieService.Service.Interfaces.Services;
using Omu.ValueInjecter;

namespace MovieService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovieController : BaseController
{
    private readonly IMovieService _movieService;
    public MovieController(IMovieService movieService,ILogger<MovieController> logger):base(logger)
    {
        _movieService = movieService; 
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<CreateMovieResponse>> CreateMovie(CreateMovieRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movieEntity = Mapper.Map<MovieDto>(request);
            var response = await _movieService.CreateMovie(movieEntity).ConfigureAwait(false);
            return Ok(Mapper.Map<CreateMovieResponse>(response));
        }
        catch (Exception ex)
        {
            Logger.LogError($"Error registering user : {ex.Message}");
            return BadRequest(ex.Message);
        }
    } 

}