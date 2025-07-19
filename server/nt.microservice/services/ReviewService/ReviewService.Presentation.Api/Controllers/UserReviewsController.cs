using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Application.Orchestration.Commands;
using ReviewService.Application.Orchestration.Queries;
using ReviewService.Presenation.Api.Models;

namespace ReviewService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserReviewsController : ControllerBase
{
    private readonly ILogger<UserReviewsController> _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UserReviewsController(IMediator mediator, IMapper mapper, ILogger<UserReviewsController> logger)
    {
        _logger = logger;
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    [Route("GetReviewsForMovie/{movieId}")]
    public ActionResult<GetReviewsForMovieResponse> GetReviewsForMovie(Guid movieId)
    {
        return default!;
    }


    public async Task<GetRecentReviewsForUsersResponse> GetRecentReviewsForUsers(GetRecentReviewsForUsersRequest request)
    {
        try
        {
            if (request.UserIds == null || !request.UserIds.Any())
            {
                _logger.LogWarning("No user IDs provided for fetching recent reviews.");
                return new GetRecentReviewsForUsersResponse();
            }
            var reviews = await _mediator.Send(new GetRecentReviewsForUsersQuery
            {
                UserIds = request.UserIds,
                Count = request.Count
            }).ConfigureAwait(false);
            return new GetRecentReviewsForUsersResponse
            {
                Reviews = reviews.Select(r => new GetRcentReviewsForUserReviewItem
                {
                    ReviewId = r.Id,
                    MovieId = r.MovieId,
                    MovieTitle = r.MovieTitle,
                    Content = r.Content,
                    Rating = r.Rating,
                    UserName = r.UserName,
                    UserDisplayName = r.UserDisplayName
                }).ToList()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while fetching recent reviews for users.");
            throw; // Consider handling this more gracefully in production code.
        }
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

            var reviewId = await _mediator.Send(new CreateReviewCommand
            {
                Review = _mapper.Map<Application.DTO.Reviews.ReviewDto>(request)
            }).ConfigureAwait(false);

            if (reviewId == Guid.Empty)
            {
                _logger.LogError("Failed to create review. Review ID is empty.");
                return BadRequest("Failed to create review.");
            }
            return Ok(new CreateReviewResponse
            {
                Id = reviewId
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while creating the review.");
            return BadRequest(e);
        }
    }
}
