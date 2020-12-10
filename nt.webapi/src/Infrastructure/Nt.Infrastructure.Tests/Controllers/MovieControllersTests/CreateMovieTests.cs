using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.Movie;
using Nt.Domain.ServiceContracts.Movie;
using Nt.Infrastructure.Tests.Helpers;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;
using Nt.Infrastructure.WebApi.Controllers;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.CreateMovie;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Controllers.MovieControllersTests
{
    public class CreateMovieTests : ControllerTestBase<MovieEntity>
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
                Id = string.Format(Utils.MockMovieIdFormat, x),
                Title = $"Movie Sample {x}",
                PlotSummary = Utils.TwoHundredCharacterString,
                Language = "English",
                ReleaseDate = ReleaseDate,
                CastAndCrew = new List<string> { "John Doe", "Jane Doe", "Jaden Doe" },
                Director = "Stephen Brown",
                Genre = "Drama",
            }).ToList();

        }

        #region Http Status Response 200
        [Theory]
        [MemberData(nameof(CreateMovieTest_ResponseStatus_200_TestData))]
        [ControllerTest(nameof(MovieController)), Feature]
        public async Task CreateMovieTest_ResponseStatus_200(CreateMovieRequest request, CreateMovieResponse expectedResult)
        {
            // Arrange
            var expectedMovieEntity = Mapper.Map<MovieEntity>(expectedResult);
            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(x => x.CreateAsync(It.IsAny<MovieEntity>()))
                .Returns(Task.FromResult(expectedMovieEntity));

            // Act
            var movieController = new MovieController(Mapper, mockMovieService.Object);
            MockModelState(request, movieController);
            var response = await movieController.CreateMovie(request);

            // Assert
            var okObjectResponse = Assert.IsType<OkObjectResult>(response.Result);
            var movieResponse = Assert.IsType<CreateMovieResponse>(okObjectResponse.Value);

            Assert.Equal(expectedResult.Id, movieResponse.Id);
            Assert.Equal(expectedResult.Title, movieResponse.Title);
            Assert.Equal(expectedResult.PlotSummary, movieResponse.PlotSummary);
            Assert.Equal(expectedResult.Language, movieResponse.Language);
            Assert.Equal(expectedResult.ReleaseDate, movieResponse.ReleaseDate);
            Assert.Equal(expectedResult.Genre, movieResponse.Genre);
            Assert.Equal(expectedResult.Director, movieResponse.Director);
            Assert.True(expectedResult.CastAndCrew.SequenceEqual(movieResponse.CastAndCrew));
        }

        public static IEnumerable<object[]> CreateMovieTest_ResponseStatus_200_TestData => new[]
        {
            new object[]
            {
                 new CreateMovieRequest
                 {
                     Title = "Movie Sample 10",
                     PlotSummary = Utils.TwoHundredCharacterString,
                     ReleaseDate = ReleaseDate,
                     Language = "English",
                     CastAndCrew = new List<string> { "John Doe", "Jane Doe", "Jaden Doe" },
                     Genre = "Drama",
                     Director = "Will Brown"
                 },
                 new CreateMovieResponse
                 {
                     Id = string.Format(Utils.MockMovieIdFormat,99),
                     Title = "Movie Sample 10",
                     PlotSummary = Utils.TwoHundredCharacterString,
                     ReleaseDate = ReleaseDate,
                     Language = "English",
                     CastAndCrew = new List<string> { "John Doe", "Jane Doe", "Jaden Doe" },
                     Genre = "Drama",
                     Director = "Will Brown"
                 }

            },
            // Existing Movie name, different year of release
            new object[]
            {
                 new CreateMovieRequest
                 {
                     Title = "Movie Sample 1",
                     PlotSummary = Utils.TwoHundredCharacterString,
                     ReleaseDate = ReleaseDate.AddYears(-1),
                     Language = "English",
                     CastAndCrew = new List<string> { "John Doe", "Jane Doe", "Jaden Doe" },
                     Genre = "Drama",
                     Director = "Will Brown"
                 },
                 new CreateMovieResponse
                 {
                     Id = string.Format(Utils.MockMovieIdFormat,99),
                     Title = "Movie Sample 1",
                     PlotSummary = Utils.TwoHundredCharacterString,
                     ReleaseDate = ReleaseDate.AddYears(-1),
                     Language = "English",
                     CastAndCrew = new List<string> { "John Doe", "Jane Doe", "Jaden Doe" },
                     Genre = "Drama",
                     Director = "Will Brown"
                 }
            },
            // Existing Movie name, different language
            new object[]
            {
                 new CreateMovieRequest
                 {
                     Title = "Movie Sample 1",
                     PlotSummary = Utils.TwoHundredCharacterString,
                     ReleaseDate = ReleaseDate,
                     Language = "Spanish",
                     CastAndCrew = new List<string> { "John Doe", "Jane Doe", "Jaden Doe" },
                     Genre = "Drama",
                     Director = "Will Brown"
                 },
                 new CreateMovieResponse
                 {
                     Id = string.Format(Utils.MockMovieIdFormat,99),
                     Title = "Movie Sample 1",
                     PlotSummary = Utils.TwoHundredCharacterString,
                     ReleaseDate = ReleaseDate,
                     Language = "Spanish",
                     CastAndCrew = new List<string> { "John Doe", "Jane Doe", "Jaden Doe" },
                     Genre = "Drama",
                     Director = "Will Brown"
                 }
            },
        };
        #endregion

        #region Http Status Response 400
        [Theory]
        [MemberData(nameof(CreateMovieTest_ResponseStatus_400_TestData))]
        [ControllerTest(nameof(MovieController)), Feature]
        public async Task CreateMovieTest_ResponseStatus_400(CreateMovieRequest request)
        {
            // Arrange
            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(x => x.CreateAsync(It.IsAny<MovieEntity>())).Throws<EntityAlreadyExistException>();

            // Act
            var movieController = new MovieController(Mapper, mockMovieService.Object);
            MockModelState(request, movieController);
            var response = await movieController.CreateMovie(request);

            // Assert
            var badRequestObject = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.IsType<SerializableError>(badRequestObject.Value);
        }


        public static IEnumerable<object[]> CreateMovieTest_ResponseStatus_400_TestData => new[]
        {
            new object[]
            {
                new CreateMovieRequest ()
            },
            //new object[]
            //{
            //    new CreateMovieRequest { Title = "Title 1" }
            //},
            //new object[]
            //{
            //    new CreateMovieRequest 
            //    { 
            //        Title = "Title 1",
            //        PlotSummary = Utils.TwoHundredCharacterString,
            //    }
            //},
            //new object[]
            //{
            //    new CreateMovieRequest
            //    {
            //        Title = "Title 1",
            //        PlotSummary = Utils.TwoHundredCharacterString,
            //        Language = "English"
            //    }
            //},
            //new object[]
            //{
            //    new CreateMovieRequest
            //    {
            //        Title = "Title 1",
            //        PlotSummary = Utils.TwoHundredCharacterString,
            //        Language = "English",
            //        ReleaseDate = ReleaseDate
            //    }
            //}
        };
        #endregion

        #region Http Status Response 409
        [Theory]
        [MemberData(nameof(CreateMovieTest_ResponseStatus_409_TestData))]
        [ControllerTest(nameof(MovieController)), Feature]
        public async Task CreateMovieTest_ResponseStatus_409(CreateMovieRequest request)
        {
            // Arrange
            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(x => x.CreateAsync(It.IsAny<MovieEntity>())).Throws<EntityAlreadyExistException>();

            // Act
            var movieController = new MovieController(Mapper, mockMovieService.Object);
            MockModelState(request, movieController);
            var response = await movieController.CreateMovie(request);

            // Assert
            var conflictObject = Assert.IsType<ConflictObjectResult>(response.Result);
            Assert.IsType<string>(conflictObject.Value);
            Assert.Equal("Movie with same meta data already exists", conflictObject.Value);
        }


        public static IEnumerable<object[]> CreateMovieTest_ResponseStatus_409_TestData => new[]
        {
            new object[]
            {
                new CreateMovieRequest
                {
                    Title = "Movie Sample 1",
                    PlotSummary = Utils.TwoHundredCharacterString,
                    ReleaseDate = ReleaseDate,
                    Language = "English",
                    CastAndCrew = new List<string>{ "Actor 1", "Actor 2" },
                    Genre = "Drama",
                    Director = "Will Brown"
                }
            }
        };
        #endregion
    }
}
