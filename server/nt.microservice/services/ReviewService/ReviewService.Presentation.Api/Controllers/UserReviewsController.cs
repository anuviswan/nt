using Microsoft.AspNetCore.Mvc;
using ReviewService.Application.Interfaces.Operations;
using ReviewService.Presenation.Api.Models;
using AutoMapper;

namespace ReviewService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserReviewsController : ControllerBase
{
    private readonly ILogger<UserReviewsController> _logger;
    private readonly IReviewService _reviewService;
    private readonly IMapper _mapper;

    public UserReviewsController(IReviewService reviewService, IMapper mapper, ILogger<UserReviewsController> logger)
    {
        _logger = logger;
        _reviewService = reviewService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("GetReviewsForMovie/{movieId}")]
    public ActionResult<GetReviewsForMovieResponse> GetReviewsForMovie(Guid movieId)
    {
        return default!;
    }

    [HttpPost]
    [Route("CreateReview")]
    public async Task<ActionResult<CreateReviewResponse>> CreateReview(CreateReviewRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewId = await _reviewService.CreateReviewAsync(_mapper.Map<ReviewService.Application.DTO.Reviews.Review>(request)).ConfigureAwait(false);

            return Ok(new CreateReviewResponse
            {
                Id = reviewId
            });
        }
        catch (Exception e)
        {
            _logger.LogError(@"An error occurred while creating the review.", e);
            return BadRequest(e);
        }
    }
}
