using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nt.Domain.Entities.Movie;
using Nt.Domain.ServiceContracts.Movie;
using Nt.Infrastructure.WebApi.Controllers;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Review;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Controllers.ReviewControllerTests
{
    public class CreateReviewTests : ControllerTestBase<ReviewEntity>
    {
        public CreateReviewTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override void InitializeCollection()
        {
            base.InitializeCollection();
            EntityCollection = new List<ReviewEntity>
            {
                new ReviewEntity
                {
                    AuthorId = "A1",
                    MovieId = "M1",
                    Id  = "R1",
                    ReviewTitle = "Good Movie",
                    ReviewDescription = "This is a good movie"
                },
            };
        }

        #region Http Response 204
        [Theory]
        [MemberData(nameof(CreateReviewSuccessTestData))]
        public async Task CreateReviewSuccessTest(CreateReviewRequest request)
        {
            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "anuviswan"),

            }, "mock"));

            var mockReviewService = new Mock<IReviewService>();
            mockReviewService.Setup(x => x.CreateAsync(It.IsAny<ReviewEntity>(), It.IsAny<string>()))
                .Returns(Task.FromResult(new ReviewEntity() with { Id = nameof(ReviewEntity.Id) }));

            
            var reviewController = new ReviewController(Mapper, mockReviewService.Object);
            reviewController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            MockModelState(request, reviewController);

            // Act
            var response = await reviewController.CreateReview(request);

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        public static IEnumerable<object[]> CreateReviewSuccessTestData => new List<object[]>
        {
            new object[]
            {
                new CreateReviewRequest
                { 
                    Description = nameof(CreateReviewRequest.Description),
                    MovieId = nameof(CreateReviewRequest.MovieId),
                    Title = nameof(CreateReviewRequest.Title)
                }
            }
        };
        #endregion


        #region Http Response 400
        [Theory]
        [MemberData(nameof(CreateReviewFailedTestData))]
        public async Task CreateReviewFailedTest(CreateReviewRequest request)
        {
            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "anuviswan"),
            }, "mock"));

            var mockReviewService = new Mock<IReviewService>();
            mockReviewService.Setup(x => x.CreateAsync(It.IsAny<ReviewEntity>(), It.IsAny<string>()))
                .Returns(Task.FromResult(new ReviewEntity() with { Id = nameof(ReviewEntity.Id) }));


            var reviewController = new ReviewController(Mapper, mockReviewService.Object);
            reviewController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            MockModelState(request, reviewController);

            // Act
            var response = await reviewController.CreateReview(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        public static IEnumerable<object[]> CreateReviewFailedTestData => new List<object[]>
        {
            new object[]
            {
                new CreateReviewRequest
                {
                    Description = null,
                    MovieId = nameof(CreateReviewRequest.MovieId),
                    Title = nameof(CreateReviewRequest.Title)
                }
            },
            new object[]
            {
                new CreateReviewRequest
                {
                    Description = null,
                    MovieId = null,
                    Title = null
                }
            }
        };
        #endregion
    }
}
