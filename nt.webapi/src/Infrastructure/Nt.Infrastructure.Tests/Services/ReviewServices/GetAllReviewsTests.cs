using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Nt.Application.Services.Movie;
using Nt.Domain.Entities.Dto;
using Nt.Domain.Entities.Movie;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.RepositoryContracts.Movie;
using Nt.Infrastructure.Tests.Helpers;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;
using Nt.Infrastructure.Tests.Helpers.TestData;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Services.ReviewServices
{
    public class GetAllReviewsTests:ServiceTestBase<ReviewEntity>
    {
        public GetAllReviewsTests(ITestOutputHelper output) : base(output)
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


        [Theory]
        [ServiceTest(nameof(MovieService)), Feature]
        [MemberData(nameof(GetReviewsSuccessTestData))]
        public async Task GetAllReviewsSuccessTest(string movieId,MovieReviewDto expectedResult)
        {
            // Arrange
            var mockReviewRepository = new Mock<IReviewRepository>();
            mockReviewRepository.Setup(x => x.GetAsync(c => c.MovieId.ToLower() == movieId.ToLower()))
                .Returns(Task.FromResult(ReviewCollection.Where(x => string.Equals(x.MovieId, movieId, StringComparison.OrdinalIgnoreCase))));

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet(x => x.ReviewRepository).Returns(mockReviewRepository.Object);

            // Act
            var reviewService = new ReviewService(unitOfWork.Object);
            var response = await reviewService.GetAllReviewsAsync(movieId);

            // Assert
            Assert.Equal(movieId, response.MovieId);
            Assert.Equal(expectedResult.MovieId, response.MovieId);

        }

        public static IEnumerable<object[]> GetReviewsSuccessTestData => new List<object[]>
        {
            new object[]
            {
                string.Format(Utils.MockMovieIdFormat,1),
                new MovieReviewDto
                {
                    MovieId = string.Format(Utils.MockMovieIdFormat,1),
                }
            },
            new object[]
            {
                string.Format(Utils.MockMovieIdFormat,5),
                new MovieReviewDto
                {
                    MovieId = string.Format(Utils.MockMovieIdFormat,5),
                }
                
            },
        };
    }
}
