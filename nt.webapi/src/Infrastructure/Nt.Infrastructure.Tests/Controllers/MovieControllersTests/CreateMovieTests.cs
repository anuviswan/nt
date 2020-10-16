using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.Movie;
using Nt.Domain.ServiceContracts.Movie;
using Nt.Infrastructure.WebApi.Controllers;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.CreateMovie;
using Nt.Infrastructure.WebApi.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Nt.Infrastructure.Tests.Controllers.MovieControllersTests
{
    public class CreateMovieTests : ControllerTestBase<MovieEntity>
    {
        public CreateMovieTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override void InitializeCollection()
        {
            base.InitializeCollection();
            EntityCollection = new ()
            {
                new(){ Title = "Title 1", Language ="Malayalam", ReleaseDate = new DateTime(2020, 8, 20) },
                new(){ Title = "Title 2", Language = "Malayalam", ReleaseDate = new DateTime(2020, 8, 20) }
            };

        }

        #region Http Status Response 201
        [Theory]
        [MemberData(nameof(CreateMovieTest_ResponseStatus_200_TestData))]
        public async Task CreateMovieTest_ResponseStatus_200(CreateMovieRequest request, CreateMovieResponse expectedResult)
        {
            // Arrange
            var expectedMovieEntity = Mapper.Map<MovieEntity>(request);
            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(x => x.CreateAsync(It.IsAny<MovieEntity>())).Returns(Task.FromResult(expectedMovieEntity));

            // Act
            var movieController = new MovieController(Mapper, mockMovieService.Object);
            MockModelState(request, movieController);
            var response = await movieController.CreateMovie(request);

            // Assert
            var okObjectResult = Assert.IsType<CreatedAtActionResult>(response.Result);
            if (okObjectResult.Value is CreateMovieResponse movieResponse)
            {
                Assert.Equal(expectedResult.Title, movieResponse.Title);
                Assert.Equal(expectedResult.Language, movieResponse.Language);
                Assert.Equal(expectedResult.ReleaseDate, movieResponse.ReleaseDate);
            }
        }


        public static IEnumerable<object[]> CreateMovieTest_ResponseStatus_200_TestData => new[]
        {
            new object[]
            {
                new CreateMovieRequest { Title = "Title 3", Language="Malayalam", ReleaseDate = new DateTime(2020,8,20)},
                new CreateMovieResponse { Title = "Title 3", Language = "Malayalam", ReleaseDate = new DateTime(2020,8,20)}
            },
            new object[]
            {
                new CreateMovieRequest { Title = "Title 1", Language="English", ReleaseDate =new DateTime(2020,8,20)  },
                new CreateMovieResponse { Title = "Title 1", Language = "English", ReleaseDate = new DateTime(2020,8,20)  }
            },
            new object[]
            {
                new CreateMovieRequest { Title = "Title 1", Language="Malayalam", ReleaseDate =new DateTime(2019,8,20)  },
                new CreateMovieResponse { Title = "Title 1", Language = "Malayalam", ReleaseDate = new DateTime(2019,8,20)   }
            },
        };
        #endregion

        #region Http Status Response 400
        [Theory]
        [MemberData(nameof(CreateMovieTest_ResponseStatus_400_TestData))]
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
            new object[]
            {
                new CreateMovieRequest { Title = "Title 1" }
            }
        };
        #endregion

        #region Http Status Response 409
        [Theory]
        [MemberData(nameof(CreateMovieTest_ResponseStatus_409_TestData))]
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
                new CreateMovieRequest { Title = "Title 1", Language="Malayalam", ReleaseDate = DateTime.Now  }
            }
        };
        #endregion

        #region Http Status Response 409
        #endregion






    }
}
