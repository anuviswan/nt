using Microsoft.AspNetCore.Mvc;
using ReviewService.Application.Interfaces.Operations;
using ReviewService.Presenation.Api.Models;

namespace ReviewService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserReviewsController : ControllerBase
{
    private readonly ILogger<UserReviewsController> _logger;

    public UserReviewsController(IReviewService reviewService, ILogger<UserReviewsController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetReviewsForMovie")]
    public ActionResult<GetReviewsForMovieResponse> GetReviewsForMovie(GetReviewsForMovieRequest request)
    {
        return default!;
    }

    [HttpPost]
    public ActionResult<CreateReviewResponse> CreateReview(CreateReviewRequest request)
    {
        return default!; 
    }

}
