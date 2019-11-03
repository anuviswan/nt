using AutoMapper;
using Moq;
using Nt.WebApi.Controllers;
using Nt.WebApi.Profiles;
using Nt.WebApi.Shared.Entities;
using Nt.WebApi.Shared.IRepositories;
using Nt.WebApi.Tests.ExtensionMethods;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nt.WebApi.Tests.Controller
{
    [TestFixture]
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
            _movieCollection = Enumerable.Range(1, 10).Select(x => new MovieEntity
            {
                DirectorName = $"Directory {x}",
                ReleaseDate = DateTime.Now,
                Title = $"Title {x}"
            }).ToList();
        }

        [Test]
        public void SearchMovie_ValidMovie_GetValidResult()
        {
            var random = new Random();
            var movie = _movieCollection[random.Next(0, _movieCollection.Count - 1)];
            var mockMovieRepository = new Mock<IMovieRepository>();

            mockMovieRepository.Setup(x => x.Get(It.IsAny<Func<MovieEntity, bool>>()))
                               .Returns<Func<MovieEntity, bool>>((predicate) => _movieCollection.Where(x => predicate(x)));
            var movieController = new MovieController(_mapper,mockMovieRepository.Object);
            var result = movieController.Search(movie.Title);

            Assert.IsTrue(result.Any());
        }

        [Test]
        public void SearchMovie_CaseSensitiveMovieTitle_GetValidResult()
        {
            var random = new Random();
            var movie = _movieCollection[random.Next(0, _movieCollection.Count - 1)];
            var mockMovieRepository = new Mock<IMovieRepository>();

            mockMovieRepository.Setup(x => x.Get(It.IsAny<Func<MovieEntity, bool>>()))
                               .Returns<Func<MovieEntity, bool>>((predicate) => _movieCollection.Where(x => predicate(x)));
            var movieController = new MovieController(_mapper, mockMovieRepository.Object);
            var result = movieController.Search(movie.Title.ChangeCase());

            Assert.IsTrue(result.Any());
        }

        [Test]
        public void SearchMovie_InvalidMovie_ShouldReturnEmpty()
        {
            var random = new Random();
            var movie = new MovieEntity { Title = "Random" };
            var mockMovieRepository = new Mock<IMovieRepository>();

            mockMovieRepository.Setup(x => x.Get(It.IsAny<Func<MovieEntity, bool>>()))
                               .Returns<Func<MovieEntity, bool>>((predicate) => _movieCollection.Where(x => predicate(x)));
            var movieController = new MovieController(_mapper, mockMovieRepository.Object);
            var result = movieController.Search(movie.Title);

            Assert.IsFalse(result.Any());
        }
    }
}
