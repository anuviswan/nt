using Nt.Domain.Entities.Dto;
using Nt.Domain.Entities.Movie;
using Nt.Domain.Entities.User;
using Nt.Domain.ServiceContracts.Movie;
using Nt.Infrastructure.Tests.Helpers;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;
using Nt.Infrastructure.Tests.Helpers.TestData;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Review.GetAllReviews;

namespace Nt.Infrastructure.Tests.Controllers.ReviewControllerTests;
public class GetAllMovieReviewsTests : ControllerTestBase<ReviewEntity>
{
    public GetAllMovieReviewsTests(ITestOutputHelper output) : base(output)
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
    [MemberData(nameof(GetAllMovieReviews_200_TestData))]
    [ControllerTest(nameof(ReviewController)), Feature]
    public async Task GetAllMovieReviews_200(string movieId, MovieReviewDto expectedReviews)
    {
        // Arrange
        var expectedResult = Mapper.Map<MovieReviewDto,GetAllReviewsResponse>(expectedReviews);
        var mockReviewService = new Mock<IReviewService>();
        mockReviewService.Setup(x => x.GetAllReviewsAsync(It.IsAny<string>()))
                        .Returns((string mId) => Task.FromResult(MockDataHelper.GetReviews(mId)));
        // Act
        var reviewController = new ReviewController(Mapper, mockReviewService.Object);
        var request = new GetAllReviewsRequest
        {
            MovieId = movieId
        };
        var response = await reviewController.GetAllMovieReviews(request);

        // Assert
        var okResponse = Assert.IsType<OkObjectResult>(response.Result);
        var result = Assert.IsAssignableFrom<GetAllReviewsResponse>(okResponse.Value);


        foreach(var (actualReview,expectedReview) in result.Reviews.Zip(expectedResult.Reviews))
        {
            Assert.Equal(expectedReview.Author.DisplayName, actualReview.Author.DisplayName);
            Assert.Equal(expectedReview.Author.Id, actualReview.Author.Id);
            Assert.Equal(expectedReview.Author.Followers, actualReview.Author.Followers);
            Assert.Equal(expectedReview.Author.UserName, actualReview.Author.UserName);

            Assert.Equal(expectedReview.Movie.Id, actualReview.Movie.Id);
            Assert.Equal(expectedReview.Movie.Title, actualReview.Movie.Title);

            Assert.Equal(expectedReview.Description, actualReview.Description);
            Assert.Equal(expectedReview.Rating, actualReview.Rating);
            Assert.Equal(expectedReview.Title, actualReview.Title);
        }
    }

    public static IEnumerable<object[]> GetAllMovieReviews_200_TestData => new []
    {
        new object[]
        {
            string.Format(Utils.MockMovieIdFormat,1),
            MockDataHelper.GetReviews(string.Format(Utils.MockMovieIdFormat,1))
        },
        new object[]
        {
            string.Format(Utils.MockMovieIdFormat,3),
            MockDataHelper.GetReviews(string.Format(Utils.MockMovieIdFormat,3))
        }
    };

    #endregion


    #region Response 200

    [Theory]
    [MemberData(nameof(GetAllMovieReviews_400_TestData))]
    [ControllerTest(nameof(ReviewController)), Feature]
    public async Task GetAllMovieReviews_400(string movieId, MovieReviewDto expectedReviews)
    {
        // Arrange
        var expectedResult = Mapper.Map<MovieReviewDto, GetAllReviewsResponse>(expectedReviews);
        var mockReviewService = new Mock<IReviewService>();
        mockReviewService.Setup(x => x.GetAllReviewsAsync(It.IsAny<string>()))
                        .Returns((string mId) => Task.FromResult(MockDataHelper.GetReviews(mId)));
        // Act
        var reviewController = new ReviewController(Mapper, mockReviewService.Object);
        var request = new GetAllReviewsRequest
        {
            MovieId = movieId
        };
        var response = await reviewController.GetAllMovieReviews(request);

        // Assert
        var okResponse = Assert.IsType<OkObjectResult>(response.Result);
        var result = Assert.IsAssignableFrom<GetAllReviewsResponse>(okResponse.Value);

        Assert.Equal(expectedResult.Reviews.Count(), result.Reviews.Count());
        Assert.False(result.Reviews.Any());
    }

    public static IEnumerable<object[]> GetAllMovieReviews_400_TestData => new []
    {
        new object[]
        {
            string.Format(Utils.MockMovieIdFormat,99),
            MockDataHelper.GetReviews(string.Format(Utils.MockMovieIdFormat,99))
        },
    };

    #endregion
}
