using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nt.Domain.Entities.Dto;
using Nt.Domain.Entities.Movie;
using Nt.Domain.Entities.User;
using Nt.Domain.ServiceContracts.Movie;
using Nt.Infrastructure.Tests.Helpers;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;
using Nt.Infrastructure.Tests.Helpers.TestData;
using Nt.Infrastructure.WebApi.Controllers;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.GetMovie;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Controllers.MovieControllersTests
{
    public class GetMovieTests:ControllerTestBase<MovieEntity>
    {
        public GetMovieTests(ITestOutputHelper output) : base(output)
        {

        }

        public static List<UserProfileEntity> UserCollection { get; set; } = MockDataHelper.UserCollection;
        public static List<ReviewEntity> ReviewCollection { get; set; } = MockDataHelper.ReviewCollection;
        public static List<MovieEntity> MovieCollection { get; set; } = MockDataHelper.MovieCollection;

        protected override void InitializeCollection()
        {
            base.InitializeCollection();
            EntityCollection = new List<MovieEntity>(MovieCollection);
        }



        #region Response 200

        [Theory]
        [MemberData(nameof(GetMovie_200_TestData))]
        [ControllerTest(nameof(MovieController)), Feature]
        public async Task GetMovie_200(string movieId,MovieEntity expectedMovie)
        {
            // Arrange
            var expectedResult = Mapper.Map<MovieEntity, GetMovieResponse>(expectedMovie);
            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(x => x.GetOne(It.IsAny<string>()))
                            .Returns((string mId) => Task.FromResult(MockDataHelper.GetMovie(mId)));
            // Act
            var movieController = new MovieController(Mapper, mockMovieService.Object);
            var response = await movieController.GetMovieMetaInfo(movieId);

            // Assert
            var okResponse = Assert.IsType<OkObjectResult>(response.Result);
            var result = Assert.IsAssignableFrom<GetMovieResponse>(okResponse.Value);

            Assert.Equal(expectedResult.Id, result.Id);
            Assert.Equal(expectedResult.Title, result.Title);
            Assert.Equal(expectedResult.PlotSummary, result.PlotSummary);
            Assert.Equal(expectedResult.Director, result.Director);
            Assert.Equal(expectedResult.Tags, result.Tags);
            Assert.Equal(expectedResult.ReleaseDate, result.ReleaseDate);
            Assert.Equal(expectedResult.Language, result.Language);
            Assert.Equal(expectedResult.CountReviews, result.CountReviews);
            Assert.Equal(expectedResult.Rating, result.Rating);
        }

        public static IEnumerable<object[]> GetMovie_200_TestData => new List<object[]>
        {
            new object[]
            {
                string.Format(Utils.MockMovieIdFormat,1),
                MockDataHelper.GetMovie(string.Format(Utils.MockMovieIdFormat,1))
            },
             new object[]
             {
                 string.Format(Utils.MockMovieIdFormat,3),
                 MockDataHelper.GetMovie(string.Format(Utils.MockMovieIdFormat,3))
             }
        };

        #endregion

        #region Response 400

        [Theory]
        [MemberData(nameof(GetMovie_400_TestData))]
        [ControllerTest(nameof(MovieController)), Feature]
        public async Task GetMovie_400(string movieId)
        {
            // Arrange
            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(x => x.GetOne(It.IsAny<string>()))
                            .Returns((string mId) => Task.FromResult(MockDataHelper.GetMovie(mId)));
            // Act
            var movieController = new MovieController(Mapper, mockMovieService.Object);
            var response = await movieController.GetMovieMetaInfo(movieId);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        public static IEnumerable<object[]> GetMovie_400_TestData => new List<object[]>
        {
            new object[]
            {
                string.Empty,
            },
        };

        #endregion
    }
}
