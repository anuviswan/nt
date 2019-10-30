﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nt.WebApi.Models.ResponseObjects;
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
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMapper mapper, IMovieDatabaseSettings movieDatabaseSettings, IMovieRepository movieRepository) : base(mapper, movieDatabaseSettings)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public IEnumerable<MovieResponse> Search(string movieName)
        {
            var result = _movieRepository.Get(x=>x.Title.Contains(movieName));
            return Mapper.Map<IEnumerable<MovieResponse>>(result);
        }
    }
}
