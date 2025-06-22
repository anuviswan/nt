using Microsoft.AspNetCore.Mvc;
using ReviewService.Presenation.Api.ViewModels;

namespace ReviewService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserReviewsController : ControllerBase
{
    private readonly ILogger<UserReviewsController> _logger;

    public UserReviewsController(ILogger<UserReviewsController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetReviewsForMovie")]
    public ActionResult<GetReviewsForMovieResponse> GetReviewsForMovie(GetReviewsForMovieRequest request)
    {
        return default!;
    }
}
