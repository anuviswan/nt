using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.Movie;
using Nt.Domain.ServiceContracts.Movie;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Review.CreateReview;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Review.GetAllReviews;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Review.GetRecentReviews;

namespace Nt.Infrastructure.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : BaseController
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IMapper mapper, IReviewService reviewService) : base(mapper) => _reviewService = reviewService;

        [HttpPost]
        [Route("CreateReview")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateReview(CreateReviewRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = await _reviewService.CreateAsync(Mapper.Map<CreateReviewRequest,ReviewEntity>(request), User.Identity.Name);
                return NoContent();
            }
            catch(EntityAlreadyExistException)
            {
                return BadRequest("Duplicate Review.Only one review is accepted for a movie per user");
            }
        }

        [HttpPost]
        [Route("GetAllReviews")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetAllReviewsResponse>> GetAllMovieReviews(GetAllReviewsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _reviewService.GetAllReviewsAsync(request.MovieId);
            return Ok(Mapper.Map<GetAllReviewsResponse>(response));
        }


        public async Task<ActionResult<GetRecentReviewsResponse>> GetRecentReviews(GetRecentReviewsRequest request)
        {
            return default;
        }
    }
}
