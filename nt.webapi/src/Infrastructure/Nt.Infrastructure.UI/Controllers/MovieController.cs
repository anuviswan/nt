﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.Movie;
using Nt.Domain.ServiceContracts.Movie;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.CreateMovie;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.SearchMovieByTitle;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nt.Infrastructure.WebApi.Controllers
{
    /// <summary>
    /// Exposes API end points for creation and maintaining of movie titles
    /// </summary>
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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
                    return Ok(Mapper.Map<CreateMovieResponse>(result));
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

        [HttpPost]
        [Route("SearchMovieByTitle")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SearchMovieByTitleResponse>>> SearchMovieByTitle(SearchMovieByTitleRequest request)
        {
            if (ModelState.IsValid)
            {
                var searchResult = await _movieService.SearchMovie(request.SearchString);
                return Ok(Mapper.Map<IEnumerable<SearchMovieByTitleResponse>>(searchResult));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
