using AutoMapper;
using Nt.WebApi.Profiles;
using Nt.WebApi.Shared.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Nt.WebApi.Tests.Controller
{
    public class MovieControllerTests
    {
        private List<MovieEntity> _movieCollection;
        private IMapper _mapper;
        [SetUp]
        public void Setup()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MovieEntityProfile());
                mc.AddProfile(new UserEntityProfile());
            });

            _mapper = mappingConfig.CreateMapper();
            InitliazeCollection();
        }


        private void InitliazeCollection()
        {
            _movieCollection = new List<MovieEntity>
            {
                new MovieEntity { Title = "Movie 1", DirectorName = "Director 1", ReleaseDate = DateTime.Now },
                new MovieEntity { Title = "Movie 2", DirectorName = "Director 2", ReleaseDate = DateTime.Now }
            };
        }
    }
}
