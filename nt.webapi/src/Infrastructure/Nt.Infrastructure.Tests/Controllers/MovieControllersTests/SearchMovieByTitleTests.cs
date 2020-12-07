using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nt.Domain.Entities.Movie;
using Nt.Domain.ServiceContracts.Movie;
using Nt.Infrastructure.Tests.Helpers;
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

        protected override void InitializeCollection()
        {
            base.InitializeCollection();
            EntityCollection = Enumerable.Range(1, 10).Select(x => new MovieEntity
            {
                Id = string.Format(Utils.MockIdFormat, x),
                Title = $"{nameof(MovieEntity.Title)} {x}",
                PlotSummary = Utils.TwoHundredCharacterString,
                Language = "English",
                Genre = x % 2 == 0 ? "Drama" : "Thriller",
                Director = "Will Brown",
                CastAndCrew = new[] { "John Doe", "Jane Doe", "Jaden Doe" },
                ReleaseDate = Utils.Date,
                Rating = 3,
                TotalReviews = 27,
            }).Concat(new MovieEntity[]
            {
                 new MovieEntity
                 {
                    Id = string.Format(Utils.MockIdFormat,1),
                    Title = $"SomeMovie {1}",
                    PlotSummary = Utils.TwoHundredCharacterString,
                    Language = "English",
                    Genre ="Thriller",
                    Director = "Will Brown",
                    CastAndCrew = new[] { "John Doe","Jane Doe","Jaden Doe" },
                    ReleaseDate = Utils.Date,
                    Rating = 3,
                    TotalReviews = 27,
                 }
            }).ToList();
        }

        #region Http Response Code 200

        [Theory]
        [MemberData(nameof(SearchMovieByTitle_200_TestData))]
        public async Task SearchMovieByTitle_200(SearchMovieByTitleRequest request,IEnumerable<SearchMovieByTitleResponse> expectedOutput)
        {
            // Arrange
            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(x => x.SearchMovie(It.IsAny<string>(),It.IsAny<int>()))
                .Returns(Task.FromResult(EntityCollection.Where(x=>x.Title.Contains(request.SearchString,StringComparison.OrdinalIgnoreCase))));

            // Act
            var movieController = new MovieController(Mapper, mockMovieService.Object);
            MockModelState(request, movieController);
            var response = await movieController.SearchMovieByTitle(request);

            // Assert
            var okResponse = Assert.IsType<OkObjectResult>(response.Result);
            var result = Assert.IsAssignableFrom<IEnumerable<SearchMovieByTitleResponse>>(okResponse.Value);
            Assert.Equal(expectedOutput.Count(), result.Count());
            Assert.Equal(expectedOutput.Select(x=>x.Title), result.Select(x => x.Title));
        }

        public static IEnumerable<object[]> SearchMovieByTitle_200_TestData => new List<object[]>
        {
            new object[]
            {
                new SearchMovieByTitleRequest{SearchString = "titl"},
                Enumerable.Range(1, 10).Select(x => new SearchMovieByTitleResponse
                {
                    Id = string.Format(Utils.MockIdFormat,x),
                    Title = $"{nameof(MovieEntity.Title)} {x}",
                    PlotSummary = Utils.TwoHundredCharacterString,
                    Language = "English",
                    Genre = x%2==0 ? "Drama":"Thriller",
                    Director = "Will Brown",
                    CastAndCrew = new[] { "John Doe","Jane Doe","Jaden Doe" },
                    ReleaseDate = Utils.Date,
                    Rating = 3,
                    TotalReviews = 27,
                }).ToList()
            },
            new object[]
            {
                new SearchMovieByTitleRequest{SearchString = "mov" },
                new List<SearchMovieByTitleResponse>
                {
                    new SearchMovieByTitleResponse
                    {
                        Id = string.Format(Utils.MockIdFormat,1),
                        Title = $"SomeMovie {1}",
                        PlotSummary = Utils.TwoHundredCharacterString,
                        Language = "English",
                        Genre ="Thriller",
                        Director = "Will Brown",
                        CastAndCrew = new[] { "John Doe","Jane Doe","Jaden Doe" },
                        ReleaseDate = Utils.Date,
                        Rating = 3,
                        TotalReviews = 27,
                    },
                }
            }
        };
        #endregion

        #region Http Response Code 400

        [Theory]
        [MemberData(nameof(SearchMovieByTitle_400_TestData))]
        public async Task SearchMovieByTitle_400(SearchMovieByTitleRequest request)
        {
            // Arrange
            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(x => x.SearchMovie(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult(EntityCollection.Where(x => x.Title.Contains(request.SearchString, StringComparison.OrdinalIgnoreCase))));

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
