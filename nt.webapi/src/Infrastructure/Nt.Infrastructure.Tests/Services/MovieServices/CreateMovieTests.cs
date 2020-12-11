using Moq;
using Nt.Application.Services.Movie;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.Movie;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.RepositoryContracts.Movie;
using Nt.Infrastructure.Tests.Helpers;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Services.MovieServices
{
    public class CreateMovieTests : ServiceTestBase<MovieEntity>
    {
        private static readonly DateTime ReleaseDate = new DateTime(2020, 8, 20);
        public CreateMovieTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override void InitializeCollection()
        {
            base.InitializeCollection();
            EntityCollection = Enumerable.Range(1, 3).Select(x => new MovieEntity
            {
                Id = string.Format(Utils.MockMovieIdFormat,x),
                Title = $"Movie Sample {x}",
                PlotSummary = Utils.TwoHundredCharacterString,
                Language = "English",
                ReleaseDate = ReleaseDate,
                CastAndCrew = new []{ "John Doe", "Jane Doe", "Jaden Doe" },
                Director = "Stephen Brown",
                Genre = "Drama",
            }).ToList();
        }

        [Theory]
        [ServiceTest(nameof(MovieService)), Feature]
        [MemberData(nameof(CreateMovieSuccessTestData))]
        public async Task CreateMovieSuccessTest(MovieEntity request,MovieEntity expected)
        {
            var mockMovieRepository = new Mock<IMovieRepository>();
            mockMovieRepository.Setup(x => x.CreateAsync(It.IsAny<MovieEntity>()))
                .Returns(Task.FromResult(expected));
            mockMovieRepository.Setup(x=>x.GetAsync(It.IsAny<Expression<Func<MovieEntity, bool>>>()))
                .Returns(Task.FromResult(EntityCollection.Where(x => x.Title.ToLower().Equals(request.Title.ToLower())
            && x.ReleaseDate.Year == request.ReleaseDate.Year
            && x.Language.ToLower().Equals(request.Language.ToLower()))));
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(x => x.MovieRepository).Returns(mockMovieRepository.Object);
            var movieService = new MovieService(mockUnitOfWork.Object);
            var result = await movieService.CreateAsync(request);

            Assert.False(string.IsNullOrEmpty(result.Id));
            Assert.Equal(expected, result);
            mockMovieRepository.Verify(x => x.CreateAsync(It.IsAny<MovieEntity>()), Times.Once);
            mockMovieRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<MovieEntity, bool>>>()), Times.Once);
        }

        public static IEnumerable<object[]> CreateMovieSuccessTestData => new List<object[]>
        {
            new []
            {
                 new MovieEntity 
                 { 
                     Title = "Movie Sample 10", 
                     PlotSummary = Utils.TwoHundredCharacterString,
                     ReleaseDate = ReleaseDate, 
                     Language = "English", 
                     CastAndCrew = new []{ "Actor 1", "Actor 2" },
                     Genre = "Drama",
                     Director = "Will Brown"
                 },
                 new MovieEntity 
                 {
                     Id = string.Format(Utils.MockMovieIdFormat,99),
                     Title = "Movie Sample 1",
                     PlotSummary = Utils.TwoHundredCharacterString,
                     ReleaseDate = ReleaseDate, 
                     Language = "English", 
                     CastAndCrew = new []{ "Actor 1", "Actor 2" },
                     Genre = "Drama",
                     Director = "Will Brown"
                 }
               
            },
            // Existing Movie name, different year of release
            new []
            {
                 new MovieEntity
                 {
                     Title = "Movie Sample 1",
                     PlotSummary = Utils.TwoHundredCharacterString,
                     ReleaseDate = ReleaseDate.AddYears(-1),
                     Language = "English",
                     CastAndCrew = new []{ "Actor 1", "Actor 2" },
                     Genre = "Drama",
                     Director = "Will Brown"
                 },
                 new MovieEntity
                 {
                     Id = string.Format(Utils.MockMovieIdFormat,99),
                     Title = "Movie Sample 1",
                     PlotSummary = Utils.TwoHundredCharacterString,
                     ReleaseDate = ReleaseDate.AddYears(-1),
                     Language = "English",
                     CastAndCrew = new []{ "Actor 1", "Actor 2" },
                     Genre = "Drama",
                     Director = "Will Brown"
                 }
            },
            // Existing Movie name, different language
            new []
            {
                 new MovieEntity
                 {
                     Title = "Movie Sample 1",
                     PlotSummary = Utils.TwoHundredCharacterString,
                     ReleaseDate = ReleaseDate,
                     Language = "Spanish",
                     CastAndCrew = new []{ "Actor 1", "Actor 2" },
                     Genre = "Drama",
                     Director = "Will Brown"
                 },
                 new MovieEntity
                 {
                     Id = string.Format(Utils.MockMovieIdFormat,99),
                     Title = "Movie Sample 1",
                     PlotSummary = Utils.TwoHundredCharacterString,
                     ReleaseDate = ReleaseDate,
                     Language = "Spanish",
                     CastAndCrew = new []{ "Actor 1", "Actor 2" },
                     Genre = "Drama",
                     Director = "Will Brown"
                 }
            },

        };


        [Theory]
        [ServiceTest(nameof(MovieService)), Feature]
        [MemberData(nameof(CreateMovieFailureTestData))]
        public async Task CreateMovieFailureTest(MovieEntity request)
        {
            // Arrange
            var mockMovieRepository = new Mock<IMovieRepository>();
            mockMovieRepository.Setup(x => x.CreateAsync(It.IsAny<MovieEntity>())).Returns(Task.FromResult(request));
            mockMovieRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<MovieEntity, bool>>>()))
                .Returns(Task.FromResult(EntityCollection.Where(x => x.Title.ToLower().Equals(request.Title.ToLower())
            && x.ReleaseDate.Year == request.ReleaseDate.Year
            && x.Language.ToLower().Equals(request.Language.ToLower()))));
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(x => x.MovieRepository).Returns(mockMovieRepository.Object);

            //Act
            var movieService = new MovieService(mockUnitOfWork.Object);
            await Assert.ThrowsAsync<EntityAlreadyExistException>(()=>movieService.CreateAsync(request));
            
            // Assert
            mockMovieRepository.Verify(x => x.CreateAsync(It.IsAny<MovieEntity>()), Times.Never);
            mockMovieRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<MovieEntity, bool>>>()), Times.Once);
        }

        public static IEnumerable<object[]> CreateMovieFailureTestData => new []
        {
            new []
            {
                 new MovieEntity
                 {
                     Title = "Movie Sample 1",
                     PlotSummary = Utils.TwoHundredCharacterString,
                     ReleaseDate = ReleaseDate,
                     Language = "English",
                     CastAndCrew = new[] { "Actor 1", "Actor 2" },
                     Genre = "Drama",
                     Director = "Will Brown"
                 }
            },
            new []
            {
                 new MovieEntity
                 {
                     Title = "Movie Sample 1",
                     PlotSummary = Utils.TwoHundredCharacterString,
                     ReleaseDate = ReleaseDate,
                     Language = "english",
                     CastAndCrew = new []{ "Actor 1", "Actor 2" },
                     Genre = "Drama",
                     Director = "Will Brown"
                 }
            },
        };
    }
}
