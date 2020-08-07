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
                new(){ Title = "Title 1", Language ="Malayalam", ReleaseDate = DateTime.Now   },
                new(){ Title = "Title 2", Language = "Malayalam", ReleaseDate = DateTime.Now }
            };

        }

        [Theory]
        [MemberData(nameof(CreateMovieTestFailureCasesTestData))]
        public async Task CreateMovieTestFailureCases(CreateMovieRequest request,CreateMovieResponse expectedResult)
        {
            // Arrange
            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(x => x.CreateAsync(It.IsAny<MovieEntity>())).Throws<EntityAlreadyExistException>();

            // Act
            var movieController = new MovieController(Mapper, mockMovieService.Object);
            MockModelState(request, movieController);
            var result = await movieController.CreateMovie(request);


            // Assert
            if(result is not IErrorInfo error)
            {
                throw new XunitException();
            }

            Assert.Equal(expectedResult.ErrorMessage, error.ErrorMessage);
            Assert.True(error.HasError);
        }


        public static IEnumerable<object[]> CreateMovieTestFailureCasesTestData => new[]
        {
            new object[]
            {
                new CreateMovieRequest (),
                new CreateMovieResponse { Title = "Title 1", Language = "Malayalam", ReleaseDate = DateTime.Now , ErrorMessage=$"The Title field is required.{Environment.NewLine}The Language field is required." }
            },
            new object[] 
            { 
                new CreateMovieRequest { Title = "Title 1" }, 
                new CreateMovieResponse { Title = "Title 1", Language = "Malayalam", ReleaseDate = DateTime.Now , ErrorMessage="The Language field is required." }  
            },
            new object[]
            {
                new CreateMovieRequest { Title = "Title 1", Language="Malayalam", ReleaseDate =DateTime.Now  },
                new CreateMovieResponse { Title = "Title 1", Language = "Malayalam", ReleaseDate = DateTime.Now , ErrorMessage="Movie with the same information already exists" }
            }
        };

        [Theory]
        [MemberData(nameof(CreateMovieTestSuccessCasesTestData))]
        public async Task CreateMovieTestSuccessCases(CreateMovieRequest request, CreateMovieResponse expectedResult)
        {
            // Arrange
            var expectedMovieEntity = Mapper.Map<MovieEntity>(request);
            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(x => x.CreateAsync(It.IsAny<MovieEntity>())).Returns(Task.FromResult(expectedMovieEntity));

            // Act
            var movieController = new MovieController(Mapper, mockMovieService.Object);
            MockModelState(request, movieController);
            var result = await movieController.CreateMovie(request);


            // Assert
            if (result is not IErrorInfo error)
            {
                throw new XunitException();
            }

            Assert.True(string.IsNullOrEmpty(error.ErrorMessage));
            Assert.False(error.HasError);
        }


        public static IEnumerable<object[]> CreateMovieTestSuccessCasesTestData => new[]
        {
            new object[]
            {
                new CreateMovieRequest { Title = "Title 3", Language="Malayalam", ReleaseDate =DateTime.Now  },
                new CreateMovieResponse { Title = "Title 3", Language = "Malayalam", ReleaseDate = DateTime.Now  }
            },
            new object[]
            {
                new CreateMovieRequest { Title = "Title 1", Language="English", ReleaseDate =DateTime.Now  },
                new CreateMovieResponse { Title = "Title 1", Language = "English", ReleaseDate = DateTime.Now  }
            },
            new object[]
            {
                new CreateMovieRequest { Title = "Title 1", Language="Malayalam", ReleaseDate =DateTime.Now.AddYears(-1)  },
                new CreateMovieResponse { Title = "Title 1", Language = "Malayalam", ReleaseDate = DateTime.Now.AddYears(-1)   }
            },

        };


    }
}
