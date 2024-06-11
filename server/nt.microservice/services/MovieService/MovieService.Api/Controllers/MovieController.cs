
using Microsoft.AspNetCore.Mvc;
using MovieService.Api.ViewModels;

namespace MovieService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{
    public MovieController()
    {
            
    }

    public ActionResult<CreateMovieResponse> CreateMovie(CreateMovieRequest request)
    {
        try
        {

        }
        catch (Exception)
        {

            throw;
        }

        return new CreateMovieResponse();
    } 
}