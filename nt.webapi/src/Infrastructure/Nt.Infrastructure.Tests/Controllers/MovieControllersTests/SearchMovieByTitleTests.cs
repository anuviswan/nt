using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nt.Domain.Entities.Movie;
using Nt.Domain.ServiceContracts.Movie;
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
            EntityCollection = new()
            {
                new() { Title = "Title 1", Language = "Malayalam", ReleaseDate = new DateTime(2020, 8, 20) },
                new() { Title = "Title 2", Language = "Malayalam", ReleaseDate = new DateTime(2020, 8, 20) },
                new() { Title = "SomeMovie 1", Language = "Malayalam", ReleaseDate = new DateTime(2020, 8, 20) }
            };
        }

        #region Http Response Code 200

        public async Task SearchMovieByTitle_200(SearchMovieByTitleRequest request,int maxCount,IEnumerable<SearchMovieByTitleResponse> expectedOutput)
        {
            // Arrange
            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(x => x.SearchMovie(It.IsAny<string>(),It.IsAny<int>()))
                .Returns(Task.FromResult(EntityCollection.Where(x=>x.Title.Contains(request.SearchString))));

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

        public IEnumerable<object[]> SearchMovieByTitle_200_TestData => new List<object[]>
        {
            new object[]
            {
                new SearchMovieByTitleRequest{SearchString = "titl"}, 
                new List<SearchMovieByTitleResponse>
                {
                    new SearchMovieByTitleResponse{ Title = "Title 1",Language = "Malayalam", ReleaseDate = new DateTime(2020, 8, 20) },
                    new SearchMovieByTitleResponse{ Title = "Title 2",Language = "Malayalam", ReleaseDate = new DateTime(2020, 8, 20) }
                }
            },
            new object[]
            {
                new SearchMovieByTitleRequest{SearchString = "titl",Criteria = new MovieSearchCriteria{ MaxItems = 1} },
                new List<SearchMovieByTitleResponse>
                {
                    new SearchMovieByTitleResponse{ Title = "Title 1",Language = "Malayalam", ReleaseDate = new DateTime(2020, 8, 20) }
                }
            }
        };
        #endregion
    }
}
