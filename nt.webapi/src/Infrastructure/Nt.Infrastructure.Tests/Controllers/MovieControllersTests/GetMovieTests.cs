using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nt.Application.Services.Movie;
using Nt.Domain.Entities.Dto;
using Nt.Domain.Entities.Movie;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.ServiceContracts.Movie;
using Nt.Infrastructure.WebApi.Controllers;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.GetMovie;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Controllers.MovieControllersTests
{
    public class GetMovieTests:ControllerTestBase<MovieEntity>
    {
        public GetMovieTests(ITestOutputHelper output) : base(output)
        {

        }

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
                Rating = 3
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
                Rating = 2
            }
        };


        protected override void InitializeCollection()
        {
            base.InitializeCollection();
            EntityCollection = new List<MovieEntity>(MovieCollection);
        }



        #region Response 200

        [Theory]
        [MemberData(nameof(GetMovie_200_TestData))]
        public async Task GetMovie_200(string movieId,MovieReviewDto expectedMovie)
        {
            // Arrange
            var expectedResult = Mapper.Map<MovieReviewDto, GetMovieResponse>(expectedMovie);
            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(x => x.GetOne(It.IsAny<string>()))
                            .Returns((string mId) => Task.FromResult(GetMovieForMovieId(mId)));
            // Act
            var movieController = new MovieController(Mapper, mockMovieService.Object);
            var response = await movieController.GetMovieMetaInfo(movieId);

            // Assert
            var okResponse = Assert.IsType<OkObjectResult>(response.Result);
            var result = Assert.IsAssignableFrom<GetMovieResponse>(okResponse.Value);

            Assert.Equal(expectedResult.Id, result.Id);
            Assert.Equal(expectedResult.Title, result.Title);
            Assert.Equal(expectedResult.PlotSummary, result.PlotSummary);
            Assert.Equal(expectedResult.Director, result.Director);
            Assert.Equal(expectedResult.Tags, result.Tags);
            Assert.Equal(expectedResult.ReleaseDate, result.ReleaseDate);
            Assert.Equal(expectedResult.Language, result.Language);
            Assert.Equal(expectedResult.Reviews.Count(), result.Reviews.Count());
            Assert.Equal(expectedResult.Reviews, result.Reviews);

            Assert.Equal(expectedResult.Rating, result.Rating);
        }

        public static IEnumerable<object[]> GetMovie_200_TestData => new List<object[]>
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

        #endregion

        #region Response 400

        [Theory]
        [MemberData(nameof(GetMovie_400_TestData))]
        public async Task GetMovie_400(string movieId)
        {
            // Arrange
            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(x => x.GetOne(It.IsAny<string>()))
                            .Returns((string mId) => Task.FromResult(GetMovieForMovieId(mId)));
            // Act
            var movieController = new MovieController(Mapper, mockMovieService.Object);
            var response = await movieController.GetMovieMetaInfo(movieId);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(response.Result);
         //   var result = Assert.IsAssignableFrom<GetMovieResponse>(badRequest.Value);
        }

        public static IEnumerable<object[]> GetMovie_400_TestData => new List<object[]>
        {
            new object[]
            {
                string.Empty,
            },
        };

        #endregion


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
                                    Actors = movie.Actors,
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
