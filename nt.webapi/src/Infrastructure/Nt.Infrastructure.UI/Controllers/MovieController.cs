using AutoMapper;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.Movie;
using Nt.Domain.ServiceContracts.Movie;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.CreateMovie;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Nt.Infrastructure.WebApi.Controllers
{
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
        [Authorize]
        public async Task<CreateMovieResponse>  CreateMovie(CreateMovieRequest movie)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var movieEntity = Mapper.Map<MovieEntity>(movie);
                    var result = await _movieService.CreateAsync(movieEntity);
                    return Mapper.Map<CreateMovieResponse>(result);
                }
                catch(EntityAlreadyExistException e)
                {
                    return base.CreateErrorResponse<CreateMovieResponse>("Movie with the same information already exists");
                }
                catch(Exception e)
                {
                    return base.CreateErrorResponse<CreateMovieResponse>($"Unexpected Error: {e.Message}");
                }
            }
            else
            {
                var errrorMessages = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage));
                return base.CreateErrorResponse<CreateMovieResponse>(string.Join(Environment.NewLine, errrorMessages));
            }
        }
    }
}
