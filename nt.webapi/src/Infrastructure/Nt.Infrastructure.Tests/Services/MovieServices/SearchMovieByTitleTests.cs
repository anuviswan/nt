using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using Nt.Application.Services.Movie;
using Nt.Domain.Entities.Movie;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.RepositoryContracts.Movie;
using Nt.Infrastructure.Tests.Helpers;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;
using Nt.Infrastructure.Tests.Helpers.TestData;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Services.MovieServices
{
    public class SearchMovieByTitleTests:ServiceTestBase<MovieEntity>
    {
        public static List<UserProfileEntity> UserCollection { get; set; } = MovieReviewCollectionHelper.UserCollection;
        public static List<ReviewEntity> ReviewCollection { get; set; } = MovieReviewCollectionHelper.ReviewCollection;
        public static List<MovieEntity> MovieCollection { get; set; } = MovieReviewCollectionHelper.MovieCollection;

        public SearchMovieByTitleTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override void InitializeCollection()
        {
            base.InitializeCollection();
            EntityCollection = new List<MovieEntity>(MovieCollection);

        }

        [Theory]
        [ServiceTest(nameof(MovieService)), Feature]
        [MemberData(nameof(SearchMovieSuccessTestData))]
        public async Task SearchMovieSuccessTest(string movieTitle,int maxCount, IEnumerable<MovieEntity> expected)
        {
            var mockMovieRepository = new Mock<IMovieRepository>();
            mockMovieRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<MovieEntity, bool>>>()))
                 .Returns(Task.FromResult(EntityCollection.Where(x => (x.Title.ToLower().Contains(movieTitle.ToLower())
                                                           || x.Title.ToLower().StartsWith(movieTitle.ToLower())))));

            var mockReviewRepository = new Mock<IReviewRepository>();
            mockReviewRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ReviewEntity, bool>>>()))
                .Returns((Expression<Func<ReviewEntity, bool>> a) => Task.FromResult(ReviewCollection.Where(a.Compile())));

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(x => x.MovieRepository).Returns(mockMovieRepository.Object);
            mockUnitOfWork.SetupGet(x => x.ReviewRepository).Returns(mockReviewRepository.Object);
            var movieService = new MovieService(mockUnitOfWork.Object);
            var result = await movieService.SearchMovie(movieTitle,maxCount);

            Assert.Equal(expected.Count(), result.Count());

            foreach (var movie in result)
            {
                var expectedMovie = expected.Single(c => c.Id.Equals(movie.Id));
                Assert.Equal(expectedMovie.Title, movie.Title);
                Assert.Equal(expectedMovie.PlotSummary, movie.PlotSummary);
                Assert.Equal(expectedMovie.Language, movie.Language);
                Assert.Equal(expectedMovie.Director, movie.Director);
                Assert.Equal(expectedMovie.Genre, movie.Genre);
                Assert.Equal(expectedMovie.ReleaseDate, movie.ReleaseDate);
                Assert.Equal(expectedMovie.Rating, movie.Rating);
                Assert.Equal(expectedMovie.TotalReviews, movie.TotalReviews);
                Assert.True(expectedMovie.CastAndCrew.OrderBy(x => x).SequenceEqual(movie.CastAndCrew.OrderBy(x => x)));
            }

        }

        public static IEnumerable<object[]> SearchMovieSuccessTestData => new List<object[]>
        {
            new object[]
            {
                 "titl",-1,
                  MovieReviewCollectionHelper.GetMovies("titl")
            },
            new object[]
            {
                 "titl",5,
                  MovieReviewCollectionHelper.GetMovies("titl").Take(5)
            },
            new object[]
            {
                 "movie",-1,MovieReviewCollectionHelper.GetMovies("movie")
            },
            new object[]
            {
                 "@@@@",-1,Enumerable.Empty<MovieEntity>()
            },
            new object[]
            {
                 null,-1,Enumerable.Empty<MovieEntity>()
            },
        };
    }
}
