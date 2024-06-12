
using Microsoft.AspNetCore.Mvc;
using MovieService.Api.ViewModels;
using MovieService.Service.Services;

namespace MovieService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;
    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public  ActionResult<CreateMovieResponse> CreateMovie(CreateMovieRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           // var response = await _movieService.CreateMovie(default).ConfigureAwait(false);
        }
        catch (Exception)
        {

            throw;
        }

        return new CreateMovieResponse();
    } 
}