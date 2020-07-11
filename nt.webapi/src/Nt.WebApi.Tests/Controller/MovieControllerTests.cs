using AutoMapper;
using Moq;
using Nt.WebApi.Controllers;
using Nt.WebApi.Models.RequestObjects;
using Nt.WebApi.Profiles;
using Nt.WebApi.Repository.Repositories;
using Nt.WebApi.Shared.Entities;
using Nt.WebApi.Shared.IRepositories;
using Nt.WebApi.Tests.ExtensionMethods;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.WebApi.Tests.Controller
{
    [TestFixture]
    public class MovieControllerTests:BaseControllerTests<MovieEntity> 
    {

        protected override void InitializeCollection()
        {
            EntityCollection = Enumerable.Range(1, 10).Select(x => new MovieEntity
            {
                DirectorName = $"Directory {x}",
                ReleaseDate = DateTime.Now,
                Title = $"Title {x}"
            }).ToList();
        }

        [Test]
        public async Task CreateMovie_ValidMovie_ShouldReturnSuccess()
        {
            var mockMovieRepository = new Mock<IMovieRepository>();
            var movieRequest = new CreateMovieRequest { Director = "John Doe", MovieName = "John's World", ReleaseDate = new DateTime(2019, 1, 1) };
            var countOfMovies = EntityCollection.Count;
            mockMovieRepository.Setup((x)=> x.CreateAsync(It.IsAny<MovieEntity>()))
                               .Returns<MovieEntity>((movie) => Task.FromResult(movie))
                               .Callback<MovieEntity>((movie) => EntityCollection.Add(movie));
            var movieController = new MovieController(Mapper, mockMovieRepository.Object);
            var result = await movieController.Create(movieRequest);

            Assert.AreEqual(movieRequest.Director, result.Director);
            Assert.AreEqual(movieRequest.MovieName, result.Title);
            Assert.AreEqual(movieRequest.ReleaseDate, result.ReleaseDate);
            Assert.IsTrue(EntityCollection.Count == countOfMovies + 1);
            Assert.IsTrue(EntityCollection.Any(x => x.Title.Equals(movieRequest.MovieName) && x.DirectorName.Equals(movieRequest.Director) && x.ReleaseDate.Equals(movieRequest.ReleaseDate)));
        }


        [Test]
        public async Task CreateMovie_ExistingMovie_ShouldReturnFalse()
        {
            var random = new Random();
            var mockMovieRepository = new Mock<IMovieRepository>();
            var randomMovie = EntityCollection[random.Next(0, EntityCollection.Count)];
            var movieRequest = Mapper.Map<CreateMovieRequest>(randomMovie);
            var countOfMovies = EntityCollection.Count;
            mockMovieRepository.Setup(x => x.CreateAsync(It.IsAny<MovieEntity>()))
                               .Returns<MovieEntity>((movie) => Task.FromResult(movie));
            mockMovieRepository.Setup(x => x.GetAsync(It.IsAny<Func<MovieEntity, bool>>()))
                              .Returns<Func<MovieEntity, bool>>((predicate) => Task.FromResult(EntityCollection.Where(x => predicate(x))));

            var movieController = new MovieController(Mapper, mockMovieRepository.Object);
            var result = await movieController.Create(movieRequest);

            Assert.IsFalse(string.IsNullOrEmpty(result.ErrorMessage));
            Assert.IsTrue(EntityCollection.Count == countOfMovies);
        }


        [Test]
        public async Task CreateMovie_ExistingMovieWithDifferentTitleCasing_ShouldReturnFalse()
        {
            var random = new Random();
            var mockMovieRepository = new Mock<IMovieRepository>();
            var randomMovie = EntityCollection[random.Next(0, EntityCollection.Count)];
            var movieRequest = new CreateMovieRequest { MovieName = randomMovie.Title.ChangeCase(), Director = randomMovie.DirectorName, ReleaseDate = randomMovie.ReleaseDate };
            var countOfMovies = EntityCollection.Count;
            mockMovieRepository.Setup(x => x.CreateAsync(It.IsAny<MovieEntity>()))
                               .Returns<MovieEntity>((movie) => Task.FromResult(movie));
            mockMovieRepository.Setup(x => x.GetAsync(It.IsAny<Func<MovieEntity, bool>>()))
                              .Returns<Func<MovieEntity, bool>>((predicate) => Task.FromResult(EntityCollection.Where(x => predicate(x))));

            var movieController = new MovieController(Mapper, mockMovieRepository.Object);
            var result = await movieController.Create(movieRequest);

            Assert.IsFalse(string.IsNullOrEmpty(result.ErrorMessage));
            Assert.IsTrue(EntityCollection.Count == countOfMovies);
        }

        [Test]
        public void SearchMovie_ValidMovie_GetValidResult()
        {
            var random = new Random();
            var movie = EntityCollection[random.Next(0, EntityCollection.Count - 1)];
            var mockMovieRepository = new Mock<IMovieRepository>();

            mockMovieRepository.Setup(x => x.GetAsync(It.IsAny<Func<MovieEntity, bool>>()))
                               .Returns<Func<MovieEntity, bool>>((predicate) => Task.FromResult(EntityCollection.Where(x => predicate(x))));
            var movieController = new MovieController(Mapper,mockMovieRepository.Object);
            var result = movieController.SearchAsync(movie.Title);

            Assert.IsTrue(result.Any());
        }

        [Test]
        public void SearchMovie_CaseSensitiveMovieTitle_GetValidResult()
        {
            var random = new Random();
            var movie = EntityCollection[random.Next(0, EntityCollection.Count - 1)];
            var mockMovieRepository = new Mock<IMovieRepository>();

            mockMovieRepository.Setup(x => x.GetAsync(It.IsAny<Func<MovieEntity, bool>>()))
                               .Returns<Func<MovieEntity, bool>>((predicate) => Task.FromResult(EntityCollection.Where(x => predicate(x))));
            var movieController = new MovieController(Mapper, mockMovieRepository.Object);
            var result = movieController.SearchAsync(movie.Title.ChangeCase());

            Assert.IsTrue(result.Any());
        }

        [Test]
        public void SearchMovie_InvalidMovie_ShouldReturnEmpty()
        {
            var random = new Random();
            var movie = new MovieEntity { Title = "Random" };
            var mockMovieRepository = new Mock<IMovieRepository>();

            mockMovieRepository.Setup(x => x.GetAsync(It.IsAny<Func<MovieEntity, bool>>()))
                               .Returns<Func<MovieEntity, bool>>((predicate) => Task.FromResult(EntityCollection.Where(x => predicate(x))));
            var movieController = new MovieController(Mapper, mockMovieRepository.Object);
            var result = movieController.SearchAsync(movie.Title);

            Assert.IsFalse(result.Any());
        }
    }
}
