using Microsoft.AspNetCore.Http;
using Nt.Domain.Entities.Dto;
using Nt.Domain.Entities.Movie;
using Nt.Domain.Entities.User;
using Nt.Domain.ServiceContracts.Movie;
using Nt.Infrastructure.Tests.Helpers;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;
using Nt.Infrastructure.Tests.Helpers.TestData;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Review.GetRecentReviews;
using System.Security.Claims;

namespace Nt.Infrastructure.Tests.Controllers.ReviewControllerTests;
public class GetRecentReviewsTests : ControllerTestBase<ReviewEntity>
{
    public GetRecentReviewsTests(ITestOutputHelper output) : base(output)
    {

    }

    public static List<UserProfileEntity> UserCollection { get; set; } = MockDataHelper.UserCollection;
    public static List<ReviewEntity> ReviewCollection { get; set; } = MockDataHelper.ReviewCollection;
    public static List<MovieEntity> MovieCollection { get; set; } = MockDataHelper.MovieCollection;

    protected override void InitializeCollection()
    {
        base.InitializeCollection();
        EntityCollection = new (ReviewCollection);
    }

    #region Response 200

    [Theory]
    [MemberData(nameof(GetRecentReviews_200_TestData))]
    [ControllerTest(nameof(ReviewController)), Feature]
    public async Task GetRecentReviews_200(string userName,int maxItems, MovieReviewDto expectedReviews)
    {
        // Arrange
        // Arrange
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new (ClaimTypes.Name, userName),

        }, "mock"));

        var expectedResult = Mapper.Map<MovieReviewDto, GetRecentReviewsResponse>(expectedReviews);
        var mockReviewService = new Mock<IReviewService>();
        mockReviewService.Setup(x => x.GetRecentReviewsFromFollowedAsync(It.IsAny<string>(),It.IsAny<int>()))
                        .Returns((string mId,int maxItems) => {
                            var reviews = MockDataHelper.GetReviews(mId);
                            return Task.FromResult(new MovieReviewDto
                            {
                                Reviews = reviews.Reviews.Take(maxItems)
                            });
                        });
        // Act
        var reviewController = new ReviewController(Mapper, mockReviewService.Object);
        var request = new GetRecentReviewsRequest
        {
            NumberOfItems = 10
        };

        reviewController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
        MockModelState(request, reviewController);

        var response = await reviewController.GetRecentReviews(request);

        // Assert
        var okResponse = Assert.IsType<OkObjectResult>(response.Result);
        var result = Assert.IsAssignableFrom<GetRecentReviewsResponse>(okResponse.Value);

        Assert.True(maxItems >= result.Reviews.Count());
        foreach (var (actualReview, expectedReview) in result.Reviews.Zip(expectedResult.Reviews))
        {
            Assert.Equal(expectedReview.Author.DisplayName, actualReview.Author.DisplayName);
            Assert.Equal(expectedReview.Author.UserName, actualReview.Author.UserName);
            Assert.Equal(expectedReview.Author.Followers, actualReview.Author.Followers);
            Assert.Equal(expectedReview.Author.Id, actualReview.Author.Id);

            Assert.Equal(expectedReview.Description, actualReview.Description);
            Assert.Equal(expectedReview.Rating, actualReview.Rating);
            Assert.Equal(expectedReview.Title, actualReview.Title);

            Assert.Equal(expectedReview.Movie.Id, actualReview.Movie.Id);
            Assert.Equal(expectedReview.Movie.Title, actualReview.Movie.Title);
        }
    }

    public static IEnumerable<object[]> GetRecentReviews_200_TestData => new List<object[]>
    {
        new object[]
        {
            "UserName 1",
            1,
            MockDataHelper.GetReviews(string.Format(Utils.MockMovieIdFormat,1))
        },
        new object[]
        {
            "UserName 3",
            1,
            MockDataHelper.GetReviews(string.Format(Utils.MockMovieIdFormat,3))
        }
    };

    #endregion
}
