using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nt.WebApi.Models.RequestObjects;
using Nt.WebApi.Models.ResponseObjects;
using Nt.WebApi.Shared.Entities;
using Nt.WebApi.Shared.IRepositories;
using Nt.WebApi.Shared.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : BaseController
    {
        private readonly IMovieRepository _movieService;

        public MovieController(IMapper mapper, IMovieRepository movieRepository) : base(mapper) => _movieService = movieRepository;


        /// <summary>
        /// Searches for Movies with the specified Movie Title
        /// </summary>
        /// <param name="movieName"></param>
        /// <returns>List of Movies which contains specified keyword in Movie Title</returns>
        [HttpGet]
        public IEnumerable<MovieResponse> Search(string movieName)
        {
            var result = _movieService.GetAsync(x=>x.Title.Contains(movieName,StringComparison.InvariantCultureIgnoreCase));
            return Mapper.Map<IEnumerable<MovieResponse>>(result);
        }

        /// <summary>
        /// Retrieves all movies in the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<MovieResponse> Get()
        {
            var result = _movieService.GetAsync();
            return Mapper.Map<IEnumerable<MovieResponse>>(result);
        }


        /// <summary>
        /// Creates new Movie with specified Title, Release Date and Director Name
        /// </summary>
        /// <param name="movie"></param>
        /// <returns>Returns Movie Entity on successfull</returns>
        [HttpPost]
        [Route("Create")]

        public async Task<MovieResponse> Create(CreateMovieRequest movie)
        {
            var movieEntity = Mapper.Map<MovieEntity>(movie);
            var movies = await _movieService.GetAsync(x => x.Title.Equals(movieEntity.Title) && x.ReleaseDate.Equals(movieEntity.ReleaseDate));
            if (movies.Any())
            {
                var response = Mapper.Map<MovieResponse>(movieEntity);
                response.ErrorMessage = "Movie with the same Title was released on same date. Verify if duplicate";
                return response;
            }
            var result = await _movieService.CreateAsync(movieEntity);
            return Mapper.Map<MovieResponse>(result);
        }


    }
}
