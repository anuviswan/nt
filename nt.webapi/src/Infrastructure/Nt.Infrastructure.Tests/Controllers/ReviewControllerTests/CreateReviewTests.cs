using Microsoft.AspNetCore.Http;
using Nt.Domain.Entities.Movie;
using Nt.Domain.ServiceContracts.Movie;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Review.CreateReview;
using System.Security.Claims;

namespace Nt.Infrastructure.Tests.Controllers.ReviewControllerTests;
public class CreateReviewTests : ControllerTestBase<ReviewEntity>
{
    public CreateReviewTests(ITestOutputHelper output) : base(output)
    {
    }

    protected override void InitializeCollection()
    {
        base.InitializeCollection();
        EntityCollection = new ()
        {
            new ReviewEntity
            {
                AuthorId = "A1",
                MovieId = "M1",
                Id  = "R1",
                ReviewTitle = "Good Movie",
                ReviewDescription = "This is a good movie",
                Rating = 2
            },
        };
    }

    #region Http Response 204
    [Theory]
    [MemberData(nameof(CreateReviewSuccessTestData))]
    [ControllerTest(nameof(ReviewController)), Feature]
    public async Task CreateReviewSuccessTest(CreateReviewRequest request)
    {
        // Arrange
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "anuviswan"),

        }, "mock"));

        var mockReviewService = new Mock<IReviewService>();
        mockReviewService.Setup(x => x.CreateAsync(It.IsAny<ReviewEntity>(), It.IsAny<string>()))
            .Returns(Task.FromResult(new ReviewEntity() { Id = nameof(ReviewEntity.Id) }));

            
        var reviewController = new ReviewController(Mapper, mockReviewService.Object);
        reviewController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
        MockModelState(request, reviewController);

        // Act
        var response = await reviewController.CreateReview(request);

        // Assert
        Assert.IsType<NoContentResult>(response);
    }

    public static IEnumerable<object[]> CreateReviewSuccessTestData => new []
    {
        new object[]
        {
            new CreateReviewRequest
            { 
                Description = nameof(CreateReviewRequest.Description),
                MovieId = nameof(CreateReviewRequest.MovieId),
                Title = nameof(CreateReviewRequest.Title),
                Rating = 2
            }
        }
    };
    #endregion


    #region Http Response 400
    [Theory]
    [MemberData(nameof(CreateReviewFailedTestData))]
    [ControllerTest(nameof(ReviewController)), Feature]
    public async Task CreateReviewFailedTest(CreateReviewRequest request)
    {
        // Arrange
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "anuviswan"),
        }, "mock"));

        var mockReviewService = new Mock<IReviewService>();
        mockReviewService.Setup(x => x.CreateAsync(It.IsAny<ReviewEntity>(), It.IsAny<string>()))
            .Returns(Task.FromResult(new ReviewEntity() { Id = nameof(ReviewEntity.Id) }));


        var reviewController = new ReviewController(Mapper, mockReviewService.Object);
        reviewController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
        MockModelState(request, reviewController);

        // Act
        var response = await reviewController.CreateReview(request);

        // Assert
        Assert.IsType<BadRequestObjectResult>(response);
    }

    public static IEnumerable<object[]> CreateReviewFailedTestData => new []
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
