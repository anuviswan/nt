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
            EntityCollection = new List<MovieEntity>
            {
                new() { Title = "Title 1", Language = "Malayalam", ReleaseDate = new DateTime(2020, 8, 20) },
                new() { Title = "Title 2", Language = "Malayalam", ReleaseDate = new DateTime(2020, 8, 20) },
                new() { Title = "SomeMovie 1", Language = "Malayalam", ReleaseDate = new DateTime(2020, 8, 20) }
            };
        }

        [Theory]
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
            Assert.Equal(expected.Select(x => x.Title), result.Select(x => x.Title));
        }

        public static IEnumerable<object[]> CreateMovieSuccessTestData => new List<object[]>
        {
            new object[]
            {
                 "titl",-1,new List<MovieEntity>
                 {
                      new() { Title = "Title 1", Language = "Malayalam", ReleaseDate = new DateTime(2020, 8, 20) },
                      new() { Title = "Title 2", Language = "Malayalam", ReleaseDate = new DateTime(2020, 8, 20) },
                 }
            },
            new object[]
            {
                 "titl",1,new List<MovieEntity>
                 {
                      new() { Title = "Title 1", Language = "Malayalam", ReleaseDate = new DateTime(2020, 8, 20) },
                 }
            },
            new object[]
            {
                 "movie",-1,new List<MovieEntity>
                 {
                      new() { Title = "SomeMovie 1", Language = "Malayalam", ReleaseDate = new DateTime(2020, 8, 20) },
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
