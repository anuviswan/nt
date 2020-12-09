using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using Nt.Application.Services.Movie;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.Movie;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.RepositoryContracts.Movie;
using Nt.Domain.RepositoryContracts.User;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Services.ReviewServices
{
    public class CreateReviewTests : ServiceTestBase<ReviewEntity>
    {
        private readonly IEnumerable<UserProfileEntity> _userProfileEntityCollection;
        public CreateReviewTests(ITestOutputHelper output) : base(output)
        {
            _userProfileEntityCollection = new List<UserProfileEntity>
            {
                new UserProfileEntity{ Id = "A1", UserName = "A1UserName", DisplayName="A1 User Display Name"},
                new UserProfileEntity{ Id = "A4", UserName = "A4UserName", DisplayName="A4 User Display Name"},
            };
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

        [Theory]
        [Trait("Category", "Service")]
        [Trait("Type", "Review")]
        [MemberData(nameof(CreateReviewSuccessTestData))]
        public async Task CreateReviewSuccessTest(ReviewEntity reviewEntity,string authorUserName,ReviewEntity expected)
        {
            // Arrange
            var mockUserRepository = new Mock<IUserProfileRepository>();
            mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
                .Returns(Task.FromResult(_userProfileEntityCollection.Where(x=>x.UserName.Equals(authorUserName,StringComparison.OrdinalIgnoreCase))));
            var mockReviewRepository = new Mock<IReviewRepository>();
            mockReviewRepository.Setup(x => x.CreateAsync(It.IsAny<ReviewEntity>()))
                .Returns(Task.FromResult(expected));
            mockReviewRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ReviewEntity, bool>>>()))
                .Returns(Task.FromResult(EntityCollection.Where(x => Equals(x.AuthorId, reviewEntity.AuthorId) 
                                        && Equals(x.MovieId, reviewEntity.MovieId))));

            
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(x => x.ReviewRepository).Returns(mockReviewRepository.Object);
            mockUnitOfWork.SetupGet(x => x.UserProfileRepository).Returns(mockUserRepository.Object);

            // Act
            var reviewService = new ReviewService(mockUnitOfWork.Object);
            var result = await reviewService.CreateAsync(reviewEntity, authorUserName);

            // Assert
            Assert.False(string.IsNullOrEmpty(result.Id));
            Assert.Equal(expected.MovieId, result.MovieId);
            Assert.Equal(expected.AuthorId, result.AuthorId);
            Assert.Equal(expected.ReviewTitle, result.ReviewTitle);
            Assert.Equal(expected.ReviewDescription, result.ReviewDescription);
            Assert.Equal(expected.Rating, result.Rating);

            mockReviewRepository.Verify(x => x.CreateAsync(It.IsAny<ReviewEntity>()), Times.Once);
            mockReviewRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<ReviewEntity,bool>>>()), Times.Once);
        }

        public static IEnumerable<object[]> CreateReviewSuccessTestData => new List<object[]>
        {
            // New Movie, New Author
            new object[]
            {
                new ReviewEntity
                {
                    AuthorId = "A4",
                    MovieId = "M4",
                    ReviewTitle = "Review Title",
                    ReviewDescription = "Review Description",
                    Rating = 2
                },
                "A4UserName",
                new ReviewEntity
                {
                    Id = "MockId",
                    AuthorId = "A4",
                    MovieId = "M4",
                    ReviewTitle = "Review Title",
                    ReviewDescription = "Review Description",
                    Rating = 2
                },
            },
            // New Movie, Existing Author
            new object[]
            {
                new ReviewEntity
                {
                    AuthorId = "A1",
                    MovieId = "M4",
                    ReviewTitle = "Review Title",
                    ReviewDescription = "Review Description",
                    Rating = 2
                },
                "A1UserName",
                new ReviewEntity
                {
                    Id = "MockId",
                    AuthorId = "A1",
                    MovieId = "M4",
                    ReviewTitle = "Review Title",
                    ReviewDescription = "Review Description",
                    Rating =2 
                },
            },
            // Existing Movie, New Author
            new object[]
            {
                new ReviewEntity
                {
                    AuthorId = "A4",
                    MovieId = "M1",
                    ReviewTitle = "Review Title",
                    ReviewDescription = "Review Description",
                    Rating = 2
                },
                "A4UserName",
                new ReviewEntity
                {
                    Id = "MockId",
                    AuthorId = "A4",
                    MovieId = "M1",
                    ReviewTitle = "Review Title",
                    ReviewDescription = "Review Description",
                    Rating = 2
                },
            },
        };


        [Theory]
        [Trait("Category", "Service")]
        [Trait("Type", "Review")]
        [MemberData(nameof(CreateReviewFailureTestData))]
        public async Task CreateReviewFailureTest(ReviewEntity reviewEntity,string authorUserName)
        {
            // Arrange
            var mockUserRepository = new Mock<IUserProfileRepository>();
            mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
                .Returns(Task.FromResult(_userProfileEntityCollection.Where(x => x.UserName.Equals(authorUserName, StringComparison.OrdinalIgnoreCase))));
            var mockReviewRepository = new Mock<IReviewRepository>();
            mockReviewRepository.Setup(x => x.CreateAsync(It.IsAny<ReviewEntity>()))
                .Returns(Task.FromResult(default(ReviewEntity)));
            mockReviewRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ReviewEntity, bool>>>()))
                .Throws<EntityAlreadyExistException>();


            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(x => x.ReviewRepository).Returns(mockReviewRepository.Object);
            mockUnitOfWork.SetupGet(x => x.UserProfileRepository).Returns(mockUserRepository.Object);

            // Act
            var reviewService = new ReviewService(mockUnitOfWork.Object);
            await Assert.ThrowsAsync<EntityAlreadyExistException>(()=>reviewService.CreateAsync(reviewEntity, authorUserName));

            // Assert
            
            mockReviewRepository.Verify(x => x.CreateAsync(It.IsAny<ReviewEntity>()), Times.Never);
            mockReviewRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<ReviewEntity, bool>>>()), Times.Once);
        }

        public static IEnumerable<object[]> CreateReviewFailureTestData => new List<object[]>
        {
            // Existing Movie, Existing Author
            new object[]
            {
                new ReviewEntity
                {
                    AuthorId = "A1",
                    MovieId = "M1",
                    ReviewTitle = "Review Title",
                    ReviewDescription = "Review Description"
                },
                "A1UserName"
            }
        };
    }
}