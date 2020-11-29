using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.Movie;
using Nt.Domain.ServiceContracts.Movie;
using Nt.Domain.ServiceContracts.User;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.CreateMovie;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.SearchMovieByTitle;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Review;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nt.Infrastructure.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : BaseController
    {
        private readonly IReviewService _reviewService;
        private readonly IMovieService _movieService;
        private readonly IUserProfileService _userProfileService;
        public ReviewController(IMapper mapper, IReviewService reviewService, IUserProfileService userProfileService, IMovieService movieService) : base(mapper)
        {
            (_reviewService, _userProfileService, _movieService) = (reviewService,userProfileService,movieService);
        }

        public async Task<ActionResult<IActionResult>> CreateReview(CreateReviewRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Identity.Name;
            return default;
        }
    }
}
