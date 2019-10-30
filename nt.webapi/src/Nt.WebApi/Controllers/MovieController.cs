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

        public MovieController(IMapper mapper, IMovieDatabaseSettings movieDatabaseSettings, IMovieRepository movieRepository) : base(mapper, movieDatabaseSettings)
        {
            _movieService = movieRepository;
        }

        [HttpGet]
        public IEnumerable<MovieResponse> Search(string movieName)
        {
            var result = _movieService.Get(x=>x.Title.Contains(movieName));
            return Mapper.Map<IEnumerable<MovieResponse>>(result);
        }


        [HttpPost]
        [Route("Create")]

        public MovieEntity CreateUser(CreateMovieRequest movie)
        {
            var movieEntity = Mapper.Map<MovieEntity>(movie);
            return _movieService.Create(movieEntity);
        }


    }
}
