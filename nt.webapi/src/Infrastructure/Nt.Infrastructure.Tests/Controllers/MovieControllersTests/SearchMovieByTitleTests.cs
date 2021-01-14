using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nt.Domain.Entities.Movie;
using Nt.Domain.Entities.User;
using Nt.Domain.ServiceContracts.Movie;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;
using Nt.Infrastructure.Tests.Helpers.TestData;
using Nt.Infrastructure.WebApi.Controllers;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.SearchMovieByTitle;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Controllers.MovieControllersTests
{
    public class SearchMovieByTitleTests : ControllerTestBase<MovieEntity>
    {
        public SearchMovieByTitleTests(ITestOutputHelper output) : base(output)
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

        #region Http Response Code 200

        [Theory]
        [MemberData(nameof(SearchMovieByTitle_200_TestData))]
        [ControllerTest(nameof(MovieController)), Feature]
        public async Task SearchMovieByTitle_200(SearchMovieByTitleRequest request,IEnumerable<MovieEntity> expectedOutput)
        {
            // Arrange
            var expectedResponse = Mapper.Map<IEnumerable<SearchMovieByTitleResponse>>(expectedOutput);
            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(x => x.SearchMovie(It.IsAny<string>(),It.IsAny<int>()))
                .Returns(Task.FromResult(EntityCollection.Where(x=>x.Title.Contains(request.SearchString,StringComparison.OrdinalIgnoreCase)).ToList()));

            // Act
            var movieController = new MovieController(Mapper, mockMovieService.Object);
            MockModelState(request, movieController);
            var response = await movieController.SearchMovieByTitle(request);

            // Assert
            var okResponse = Assert.IsType<OkObjectResult>(response.Result);
            var result = Assert.IsAssignableFrom<IEnumerable<SearchMovieByTitleResponse>>(okResponse.Value);
            Assert.Equal(expectedResponse.Count(), result.Count());
            Assert.Equal(expectedResponse.Select(x=>x.Title), result.Select(x => x.Title));
            Assert.Equal(expectedResponse.Select(x => x.PlotSummary), result.Select(x => x.PlotSummary));
        }

        public static IEnumerable<object[]> SearchMovieByTitle_200_TestData => new List<object[]>
        {
            new object[]
            {
                new SearchMovieByTitleRequest{SearchString = "title"},
                MockDataHelper.GetMovies("title")
            },
            new object[]
            {
                new SearchMovieByTitleRequest{SearchString = "mov" },
                MockDataHelper.GetMovies("mov")
            }
        };
        #endregion

        #region Http Response Code 400

        [Theory]
        [MemberData(nameof(SearchMovieByTitle_400_TestData))]
        [ControllerTest(nameof(MovieController)), Feature]
        public async Task SearchMovieByTitle_400(SearchMovieByTitleRequest request)
        {
            // Arrange
            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(x => x.SearchMovie(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult(EntityCollection.Where(x => request?.SearchString == null ? false: x.Title.Contains(request.SearchString, StringComparison.OrdinalIgnoreCase)).ToList()));

            // Act
            var movieController = new MovieController(Mapper, mockMovieService.Object);
            MockModelState(request, movieController);
            var response = await movieController.SearchMovieByTitle(request);

            // Assert
            var badRequestObject = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.IsType<SerializableError>(badRequestObject.Value);
        }

        public static IEnumerable<object[]> SearchMovieByTitle_400_TestData => new List<object[]>
        {
            new object[]
            {
                new SearchMovieByTitleRequest{SearchString = "t"}
            },
            new object[]
            {
                new SearchMovieByTitleRequest{SearchString = string.Empty },
            },
            new object[]
            {
                new SearchMovieByTitleRequest{SearchString = null },
            }
        };
        #endregion


    }
}
