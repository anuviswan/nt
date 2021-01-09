using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Nt.Infrastructure.WebApi.ViewModels.Areas.Review.GetAllReviews;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Controllers.ReviewControllerTests
{
    public class GetAllMovieReviewsTest : ControllerTestBase<ReviewEntity>
    {
        public GetAllMovieReviewsTest(ITestOutputHelper output) : base(output)
        {

        }

        public static List<UserProfileEntity> UserCollection { get; set; } = MovieReviewCollectionHelper.UserCollection;
        public static List<ReviewEntity> ReviewCollection { get; set; } = MovieReviewCollectionHelper.ReviewCollection;
        public static List<MovieEntity> MovieCollection { get; set; } = MovieReviewCollectionHelper.MovieCollection;

        protected override void InitializeCollection()
        {
            base.InitializeCollection();
            EntityCollection = new List<ReviewEntity>(ReviewCollection);
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
            mockReviewService.Setup(x => x.GetAllReviews(It.IsAny<string>()))
                            .Returns((string mId) => Task.FromResult(MovieReviewCollectionHelper.GetReviews(mId)));
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


            Assert.Equal(movieId, result.MovieId);
            foreach(var (actualReview,expectedReview) in result.Reviews.Zip(expectedResult.Reviews))
            {
                Assert.Equal(expectedReview.AuthorDisplayName, actualReview.AuthorDisplayName);
                Assert.Equal(expectedReview.AuthorId, actualReview.AuthorId);
                Assert.Equal(expectedReview.Description, actualReview.Description);
                Assert.Equal(expectedReview.Rating, actualReview.Rating);
                Assert.Equal(expectedReview.Title, actualReview.Title);
            }
        }

        public static IEnumerable<object[]> GetAllMovieReviews_200_TestData => new List<object[]>
        {
            new object[]
            {
                string.Format(Utils.MockMovieIdFormat,1),
                MovieReviewCollectionHelper.GetReviews(string.Format(Utils.MockMovieIdFormat,1))
            },
             new object[]
             {
                 string.Format(Utils.MockMovieIdFormat,3),
                 MovieReviewCollectionHelper.GetReviews(string.Format(Utils.MockMovieIdFormat,3))
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
            mockReviewService.Setup(x => x.GetAllReviews(It.IsAny<string>()))
                            .Returns((string mId) => Task.FromResult(MovieReviewCollectionHelper.GetReviews(mId)));
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


            Assert.Equal(movieId, result.MovieId);
            Assert.Equal(expectedResult.Reviews.Count(), result.Reviews.Count());
            Assert.False(result.Reviews.Any());
        }

        public static IEnumerable<object[]> GetAllMovieReviews_400_TestData => new List<object[]>
        {
            new object[]
            {
                string.Format(Utils.MockMovieIdFormat,99),
                MovieReviewCollectionHelper.GetReviews(string.Format(Utils.MockMovieIdFormat,99))
            },
        };

        #endregion
    }
}
