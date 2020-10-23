using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.Movie;
using Nt.Domain.ServiceContracts.Movie;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.CreateMovie;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.Infrastructure.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController:BaseController
    {
        private readonly IMovieService _movieService;
        public MovieController(IMapper mapper, IMovieService movieService) : base(mapper) => _movieService = movieService;

        /// <summary>
        /// Creates a new Movie if the movie doesn't exist. Duplicate movies are idenitified using Title,Language and Year Of Release 
        /// </summary>
        /// <param name="movie">Movie to be added</param>
        /// <returns>Returns Movie details.Error Message would be set in case of an error</returns>
        [HttpPost]
        [Route("CreateMovie")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateMovieResponse>>  CreateMovie(CreateMovieRequest movie)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var movieEntity = Mapper.Map<MovieEntity>(movie);
                    var result = await _movieService.CreateAsync(movieEntity);
                    return CreatedAtAction(GetLocationString(this), Mapper.Map<CreateMovieResponse>(result));
                }
                catch (EntityAlreadyExistException)
                {
                    return Conflict("Movie with same meta data already exists");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
