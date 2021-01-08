using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.Movie;
using Nt.Domain.ServiceContracts.Movie;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Review.CreateReview;

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

            }
}
