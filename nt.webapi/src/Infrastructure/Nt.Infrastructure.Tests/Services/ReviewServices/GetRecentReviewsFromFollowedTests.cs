using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using Nt.Application.Services.Movie;
using Nt.Domain.Entities.Dto;
using Nt.Domain.Entities.Movie;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.RepositoryContracts.Movie;
using Nt.Domain.RepositoryContracts.User;
using Nt.Infrastructure.Tests.Helpers;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;
using Nt.Infrastructure.Tests.Helpers.TestData;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Services.ReviewServices
{
    public class GetRecentReviewsFromFollowedTests : ServiceTestBase<ReviewEntity>
    {
        public GetRecentReviewsFromFollowedTests(ITestOutputHelper output) : base(output)
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

        [Theory]
        [ServiceTest(nameof(ReviewService)), Feature]
        [MemberData(nameof(GetRecentReviewsFromFollowedSuccessTestData))]
        public async Task GetRecentReviewsFromFollowedSuccessTest(string movieId, MovieReviewDto expectedResult)
        {
            // Arrange
            var mockReviewRepository = new Mock<IReviewRepository>();
            mockReviewRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ReviewEntity, bool>>>()))
                .Returns(Task.FromResult(ReviewCollection.Where(x => string.Equals(x.MovieId, movieId, StringComparison.OrdinalIgnoreCase))));

            var mockMovieRepository = new Mock<IMovieRepository>();
            mockMovieRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<MovieEntity, bool>>>()))
                .Returns(Task.FromResult(MovieCollection.Where(x => string.Equals(x.Id, movieId, StringComparison.OrdinalIgnoreCase))));

            var mockUserRepository = new Mock<IUserProfileRepository>();
            mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
                                           .Returns((Expression<Func<UserProfileEntity, bool>> x) =>
                                           Task.FromResult(UserCollection.AsQueryable<UserProfileEntity>().Where(x).AsEnumerable()));

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet(x => x.ReviewRepository).Returns(mockReviewRepository.Object);
            unitOfWork.SetupGet(x => x.MovieRepository).Returns(mockMovieRepository.Object);
            unitOfWork.SetupGet(x => x.UserProfileRepository).Returns(mockUserRepository.Object);

            // Act
            var reviewService = new ReviewService(unitOfWork.Object);
            var response = await reviewService.GetRecentReviewsFromFollowedAsync(movieId);

            // Assert
            Assert.Equal(expectedResult.Reviews.Count(), response.Reviews.Count());

            foreach (var (expected, actual) in expectedResult.Reviews.OrderBy(x => x.Id).Zip(response.Reviews.OrderBy(x => x.Id)))
            {
                Assert.Equal(expected.Title, actual.Title);
                Assert.Equal(expected.Description, actual.Description);
                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.Rating, actual.Rating);
                Assert.Equal(expected.UpvotedBy.OrderBy(x => x), actual.UpvotedBy.OrderBy(x => x));
                Assert.Equal(expected.DownvotedBy.OrderBy(x => x), actual.DownvotedBy.OrderBy(x => x));

                Assert.Equal(expected.Movie.Id, actual.Movie.Id);
                Assert.Equal(expected.Movie.Title, actual.Movie.Title);
            }

        }

        public static IEnumerable<object[]> GetRecentReviewsFromFollowedSuccessTestData => new List<object[]>
        {
            new object[]
            {
                Utils.GenerateUserIdString(1),
                MockDataHelper.GetReviewsByUser(Utils.GenerateUserIdString(3))
            },
            new object[]
            {
                Utils.GenerateUserIdString(5),
                new MovieReviewDto{Reviews= Enumerable.Empty<ReviewDto>()}
            },
        };
    }
}
