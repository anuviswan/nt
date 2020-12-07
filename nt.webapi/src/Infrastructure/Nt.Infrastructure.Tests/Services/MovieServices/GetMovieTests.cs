﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Nt.Application.Services.Movie;
using Nt.Domain.Entities.Dto;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.Movie;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.RepositoryContracts.Movie;
using Nt.Domain.RepositoryContracts.User;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Services.MovieServices
{
    public class GetMovieTests:ServiceTestBase<MovieEntity>
    {
        public static List<UserProfileEntity> UserCollection { get; set; } = GetUserCollection();
        public static List<ReviewEntity> ReviewCollection { get; set; } = GetReviewCollection();
        public static List<MovieEntity> MovieCollection { get; set; } = GetMovieCollection();


        private static List<UserProfileEntity> GetUserCollection() => Enumerable.Range(1, 10).Select(x => new UserProfileEntity
        {
            Id = $"A{x}",
            UserName = $"{nameof(UserProfileEntity.UserName)} {x}",
            DisplayName = $"{nameof(UserProfileEntity.DisplayName)} {x}",
            Bio = $"{nameof(UserProfileEntity.Bio)} {x}",
            IsDeleted = false,
        }).ToList();

        private static List<MovieEntity> GetMovieCollection() => new List<MovieEntity>
        {
            new MovieEntity { Id = "M1", Title = "Movie Sample 1",Language = "Malayalam", ReleaseDate = DateTime.Now },
            new MovieEntity { Id = "M2", Title = "Movie Sample 2",Language = "Malayalam", ReleaseDate = DateTime.Now },
            new MovieEntity { Id = "M3", Title = "Movie Sample 3",Language = "Malayalam", ReleaseDate = DateTime.Now },
            new MovieEntity { Id = "M4", Title = "Movie Sample 3",Language = "Malayalam", ReleaseDate = DateTime.Now },
            new MovieEntity { Id = "M4", Title = "Movie Sample 3",Language = "Malayalam", ReleaseDate = DateTime.Now },
        };


        private static List<ReviewEntity> GetReviewCollection() => new List<ReviewEntity>
        {
            new ReviewEntity
            {
                Id = "R1",
                MovieId = "M1",
                AuthorId="A1",
                ReviewTitle = $"{nameof(ReviewEntity.ReviewTitle)} 01",
                ReviewDescription = $"{nameof(ReviewEntity.ReviewDescription)} 01",
                DownVotedBy = new List<string>{"A2","A3","A4" },
                UpVotedBy = new List<string>{"A5","A6"},
                Rating = 4
            },
            new ReviewEntity
            {
                Id = "R2",
                MovieId = "M1",
                AuthorId="A7",
                ReviewTitle = $"{nameof(ReviewEntity.ReviewTitle)} 02",
                ReviewDescription = $"{nameof(ReviewEntity.ReviewDescription)} 02",
                DownVotedBy = new List<string>{"A2","A3","A4" },
                UpVotedBy = new List<string>{"A1","A5","A6"},
                Rating = 4
            },
            new ReviewEntity
            {
                Id = "R3",
                MovieId = "M2",
                AuthorId="A7",
                ReviewTitle = $"{nameof(ReviewEntity.ReviewTitle)} 01",
                ReviewDescription = $"{nameof(ReviewEntity.ReviewDescription)} 01",
                DownVotedBy = new List<string>{"A2","A3","A4" },
                UpVotedBy = new List<string>{"A1","A5","A6"},
                Rating = 4
            }
        };
        public GetMovieTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override void InitializeCollection()
        {
            base.InitializeCollection();
            EntityCollection = new List<MovieEntity>(MovieCollection);
        }


        [Theory]
        [Trait("Category", "Service")]
        [Trait("Type", "Movie")]
        [MemberData(nameof(GetMovieSuccessTestData))]
        public async Task GetMovieSuccessTest(string movieId,MovieReviewDto expectedResult)
        {
            // Arrange
            var mockMovieRepository = new Mock<IMovieRepository>();
            mockMovieRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<MovieEntity, bool>>>()))
                                .Returns(Task.FromResult(EntityCollection.Where(x => x.Id == movieId)));

            var mockReviewRepository = new Mock<IReviewRepository>();
            mockReviewRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ReviewEntity, bool>>>()))
                                .Returns(Task.FromResult(ReviewCollection.Where(x => x.MovieId == movieId)));

            var mockUserRepository = new Mock<IUserProfileRepository>();
            mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
                                .Returns((Expression<Func<UserProfileEntity, bool>> x) => 
                                Task.FromResult(UserCollection.AsQueryable<UserProfileEntity>().Where(x).AsEnumerable()));

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet(x => x.MovieRepository).Returns(mockMovieRepository.Object);
            unitOfWork.SetupGet(x => x.UserProfileRepository).Returns(mockUserRepository.Object);
            unitOfWork.SetupGet(x => x.ReviewRepository).Returns(mockReviewRepository.Object);

            // Act
            var movieService = new MovieService(unitOfWork.Object);
            var result = await movieService.GetOne(movieId);

            // Assert
            Assert.Equal(expectedResult.Id, result.Id);
            Assert.Equal(expectedResult.Title, result.Title);
            Assert.Equal(expectedResult.PlotSummary, result.PlotSummary);
            Assert.Equal(expectedResult.Director, result.Director);
            Assert.Equal(expectedResult.CastAndCrew, result.CastAndCrew);
            Assert.Equal(expectedResult.ReleaseDate, result.ReleaseDate);
            Assert.Equal(expectedResult.Language, result.Language);
            Assert.Equal(expectedResult.Reviews.Count(), result.Reviews.Count());
            Assert.Equal(expectedResult.Reviews, result.Reviews);
        }

        public static IEnumerable<object[]> GetMovieSuccessTestData => new List<object[]>
        {
            new object[]
            {
                "M1",
                GetMovieForMovieId("M1")
            },
             new object[]
             {
                 "M3",
                 GetMovieForMovieId("M3")
             }
        };

        [Theory]
        [Trait("Category", "Service")]
        [Trait("Type", "Movie")]
        [MemberData(nameof(GetMovieFailureTestData))]
        public async Task GetMovieFailureTest(string movieId,Exception exception)
        {
            var mockMovieRepository = new Mock<IMovieRepository>();
            mockMovieRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<MovieEntity, bool>>>()))
                                .Returns(Task.FromResult(EntityCollection.Where(x => x.Id == movieId)));

            var mockReviewRepository = new Mock<IReviewRepository>();
            mockReviewRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ReviewEntity, bool>>>()))
                                .Returns(Task.FromResult(ReviewCollection.Where(x => x.MovieId == movieId)));

            var mockUserRepository = new Mock<IUserProfileRepository>();
            mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
                                .Returns((Expression<Func<UserProfileEntity, bool>> x) =>
                                Task.FromResult(UserCollection.AsQueryable<UserProfileEntity>().Where(x).AsEnumerable()));


            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet(x => x.MovieRepository).Returns(mockMovieRepository.Object);
            unitOfWork.SetupGet(x => x.UserProfileRepository).Returns(mockUserRepository.Object);
            unitOfWork.SetupGet(x => x.ReviewRepository).Returns(mockReviewRepository.Object);

            // Act
            var movieService = new MovieService(unitOfWork.Object);
            await Assert.ThrowsAsync(exception.GetType(), () => movieService.GetOne(movieId));
        }


        public static IEnumerable<object[]> GetMovieFailureTestData => new List<object[]>
        {
            new object[]
            {
                "M4",
                new MultipleEntityFoundException()
            },
            new object[]
            {
                "M5",
                new EntityNotFoundException()
            }
        };


        private static MovieReviewDto GetMovieForMovieId(string movieId)
        {
            return MovieCollection.Where(c => c.Id == movieId)
                                .Select(movie => new MovieReviewDto
                                {
                                    Id = movie.Id,
                                    Director = movie.Director,
                                    PlotSummary = movie.PlotSummary,
                                    Language = movie.Language,
                                    Title = movie.Title,
                                    ReleaseDate = movie.ReleaseDate,
                                    CastAndCrew = movie.CastAndCrew,
                                    Reviews = ReviewCollection.Where(review => review.MovieId == movieId)
                                                               .Select(review => new ReviewDto
                                                               {
                                                                   Id = review.Id,
                                                                   Description = review.ReviewDescription,
                                                                   Title = review.ReviewTitle,
                                                                   DownvotedBy = review.DownVotedBy,
                                                                   UpvotedBy = review.UpVotedBy,
                                                                   Author = UserCollection.Where(user => user.Id == review.AuthorId)
                                                                                           .Select(user => new UserDto
                                                                                           {
                                                                                               Id = user.Id,
                                                                                               UserName = user.UserName,
                                                                                               DisplayName = user.DisplayName
                                                                                           }).Single()
                                                               }).ToList()
                                }).Single();
        }
    }
}
