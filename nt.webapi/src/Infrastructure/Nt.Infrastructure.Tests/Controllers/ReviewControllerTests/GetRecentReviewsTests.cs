﻿using System;
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
using Nt.Infrastructure.WebApi.ViewModels.Areas.Review.GetRecentReviews;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Controllers.ReviewControllerTests
{
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
            EntityCollection = new List<ReviewEntity>(ReviewCollection);
        }

        #region Response 200

        [Theory]
        [MemberData(nameof(GetRecentReviews_200_TestData))]
        [ControllerTest(nameof(ReviewController)), Feature]
        public async Task GetRecentReviews_200(string movieId, MovieReviewDto expectedReviews)
        {
            // Arrange
            var expectedResult = Mapper.Map<MovieReviewDto, GetRecentReviewsResponse>(expectedReviews);
            var mockReviewService = new Mock<IReviewService>();
            mockReviewService.Setup(x => x.GetRecentReviewsFromFollowed(It.IsAny<string>()))
                            .Returns((string mId) => Task.FromResult(MockDataHelper.GetReviews(mId)));
            // Act
            var reviewController = new ReviewController(Mapper, mockReviewService.Object);
            var request = new GetRecentReviewsRequest
            {
                NumberOfItems = 10
            };
            var response = await reviewController.GetRecentReviews(request);

            // Assert
            var okResponse = Assert.IsType<OkObjectResult>(response.Result);
            var result = Assert.IsAssignableFrom<GetRecentReviewsResponse>(okResponse.Value);


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
                Utils.GenerateUserIdString(1),
                MockDataHelper.GetReviews(string.Format(Utils.MockMovieIdFormat,1))
            },
             new object[]
             {
                 string.Format(Utils.MockMovieIdFormat,3),
                 MockDataHelper.GetReviews(string.Format(Utils.MockMovieIdFormat,3))
             }
        };

        #endregion
    }
}
