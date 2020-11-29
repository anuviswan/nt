using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Nt.Application.Services.Movie;
using Nt.Domain.Entities.Movie;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.RepositoryContracts.Movie;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Services.ReviewServices
{
    public class CreateReviewTests : ServiceTestBase<ReviewEntity>
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
                    AuthorId = "U1",
                    MovieId = "M1",
                    Id  = "R1",
                    ReviewTitle = "Good Movie",
                    ReviewDescription = "This is a good movie"
                },
                new ReviewEntity
                {
                    AuthorId = "U1",
                    MovieId = "M2",
                    Id  = "R2",
                    ReviewTitle = "Average Movie",
                    ReviewDescription = "This is a average movie"
                },
                new ReviewEntity
                {
                    AuthorId = "U2",
                    MovieId = "M2",
                    Id  = "R3",
                    ReviewTitle = "Average Movie",
                    ReviewDescription = "This is a average movie"
                },
                new ReviewEntity
                {
                    AuthorId = "U2",
                    MovieId = "M1",
                    Id  = "R4",
                    ReviewTitle = "Good Movie",
                    ReviewDescription = "This is a average movie"
                },
            };
        }

        [Theory]
        [MemberData(nameof(CreateReviewSuccessTestData))]
        public async Task CreateReviewSuccessTest(ReviewEntity reviewEntity,ReviewEntity expected)
        {
            // Arrange
            var mockReviewRepository = new Mock<IReviewRepository>();
            mockReviewRepository.Setup(x => x.CreateAsync(It.IsAny<ReviewEntity>()))
                .Returns(Task.FromResult(expected));
            mockReviewRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ReviewEntity, bool>>>()))
                .Returns(Task.FromResult(EntityCollection.Where(x => Equals(x.AuthorId, reviewEntity.AuthorId) 
                                        && Equals(x.MovieId, reviewEntity.MovieId))));

            
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(x => x.ReviewRepository).Returns(mockReviewRepository.Object);

            // Act
            var reviewService = new ReviewService(mockUnitOfWork.Object);
            var result = await reviewService.CreateAsync(reviewEntity);

            // Assert
            Assert.False(string.IsNullOrEmpty(result.Id));
            Assert.Equal(expected.MovieId, result.MovieId);
            Assert.Equal(expected.AuthorId, result.AuthorId);
            Assert.Equal(expected.ReviewTitle, result.ReviewTitle);
            Assert.Equal(expected.ReviewDescription, result.ReviewDescription);

            mockReviewRepository.Verify(x => x.CreateAsync(It.IsAny<ReviewEntity>()), Times.Once);
            mockReviewRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<ReviewEntity,bool>>>()), Times.Once);
        }

        public static IEnumerable<object[]> CreateReviewSuccessTestData => new List<object[]>
        {
            new[]
            {
                new ReviewEntity
                {
                    AuthorId = "A4",
                    MovieId = "M4",
                    ReviewTitle = "Review Title",
                    ReviewDescription = "Review Description"
                },
                new ReviewEntity
                {
                    Id = "MockId",
                    AuthorId = "A4",
                    MovieId = "M4",
                    ReviewTitle = "Review Title",
                    ReviewDescription = "Review Description"
                },
            }
        };
    }
}