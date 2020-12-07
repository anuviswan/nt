using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using Nt.Application.Services.Movie;
using Nt.Domain.Entities.Movie;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.RepositoryContracts.Movie;
using Nt.Infrastructure.Tests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Services.MovieServices
{
    public class SearchMovieByTitleTests:ServiceTestBase<MovieEntity>
    {
        public SearchMovieByTitleTests(ITestOutputHelper output) : base(output)
        {
        }

        protected override void InitializeCollection()
        {
            base.InitializeCollection();
            EntityCollection = Enumerable.Range(1, 10).Select(x => new MovieEntity
            {
                Id = string.Format(Utils.MockIdFormat,x),
                Title = $"{nameof(MovieEntity.Title)} {x}",
                PlotSummary = Utils.TwoHundredCharacterString,
                Language = "English",
                Genre = x%2==0 ? "Drama":"Thriller",
                Director = "Will Brown",
                CastAndCrew = new[] { "John Doe","Jane Doe","Jaden Doe" },
                ReleaseDate = Utils.Date,
                Rating = 3,
                TotalReviews = 27,
            }).Concat(new MovieEntity[]
            {
                 new MovieEntity
                 {
                    Id = string.Format(Utils.MockIdFormat,1),
                    Title = $"SomeMovie {1}",
                    PlotSummary = Utils.TwoHundredCharacterString,
                    Language = "English",
                    Genre ="Thriller",
                    Director = "Will Brown",
                    CastAndCrew = new[] { "John Doe","Jane Doe","Jaden Doe" },
                    ReleaseDate = Utils.Date,
                    Rating = 3,
                    TotalReviews = 27,
                 }
            }).ToList();
            
        }

        [Theory]
        [Trait("Category", "Service")]
        [Trait("Type", "Movie")]
        [MemberData(nameof(CreateMovieSuccessTestData))]
        public async Task CreateMovieSuccessTest(string movieTitle,int maxCount, IEnumerable<MovieEntity> expected)
        {
            var mockMovieRepository = new Mock<IMovieRepository>();
            mockMovieRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<MovieEntity, bool>>>()))
                 .Returns(Task.FromResult(EntityCollection.Where(x => (x.Title.ToLower().Contains(movieTitle.ToLower())
                                                           || x.Title.ToLower().StartsWith(movieTitle.ToLower())))));

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(x => x.MovieRepository).Returns(mockMovieRepository.Object);
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

        public static IEnumerable<object[]> CreateMovieSuccessTestData => new List<object[]>
        {
            new object[]
            {
                 "titl",-1,
                  Enumerable.Range(1, 10).Select(x => new MovieEntity
                  {
                        Id = string.Format(Utils.MockIdFormat,x),
                        Title = $"{nameof(MovieEntity.Title)} {x}",
                        PlotSummary = Utils.TwoHundredCharacterString,
                        Language = "English",
                        Genre = x%2==0 ? "Drama":"Thriller",
                        Director = "Will Brown",
                        CastAndCrew = new[] { "John Doe","Jane Doe","Jaden Doe" },
                        ReleaseDate = Utils.Date,
                        Rating = 3,
                        TotalReviews = 27,
                  }).ToList()
            },
            new object[]
            {
                 "titl",5,
                  Enumerable.Range(1, 5).Select(x => new MovieEntity
                  {
                        Id = string.Format(Utils.MockIdFormat,x),
                        Title = $"{nameof(MovieEntity.Title)} {x}",
                        PlotSummary = Utils.TwoHundredCharacterString,
                        Language = "English",
                        Genre = x%2==0 ? "Drama":"Thriller",
                        Director = "Will Brown",
                        CastAndCrew = new[] { "John Doe","Jane Doe","Jaden Doe" },
                        ReleaseDate = Utils.Date,
                        Rating = 3,
                        TotalReviews = 27,
                  }).ToList()
            },
            new object[]
            {
                 "movie",-1,new List<MovieEntity>
                 {
                     new MovieEntity
                     {
                        Id = string.Format(Utils.MockIdFormat,1),
                        Title = $"SomeMovie {1}",
                        PlotSummary = Utils.TwoHundredCharacterString,
                        Language = "English",
                        Genre ="Thriller",
                        Director = "Will Brown",
                        CastAndCrew = new[] { "John Doe","Jane Doe","Jaden Doe" },
                        ReleaseDate = Utils.Date,
                        Rating = 3,
                        TotalReviews = 27,
                     }
                 }
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
