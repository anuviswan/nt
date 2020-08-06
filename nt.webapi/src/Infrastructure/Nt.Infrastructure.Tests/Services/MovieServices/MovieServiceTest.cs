using Moq;
using Nt.Application.Services.Movie;
using Nt.Domain.Entities.Movie;
using Nt.Domain.RepositoryContracts.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Services.MovieServices
{
    public class MovieServiceTest : ServiceTestBase<MovieEntity>
    {
        public MovieServiceTest(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [MemberData(nameof(CreateMovieSuccessTestData))]
        public async Task CreateMovieSuccessTest(MovieEntity request,MovieEntity expected)
        {
            var mockMovieRepository = new Mock<IMovieRepository>();
            mockMovieRepository.Setup(x => x.CreateAsync(It.IsAny<MovieEntity>())).Returns(Task.FromResult(expected));
            var movieService = new MovieService();
            var result = await movieService.CreateAsync(request);


            Assert.False(string.IsNullOrEmpty(result.Id));
            Assert.Equal(expected.Title, request.Title);
            mockMovieRepository.Verify(x => x.CreateAsync(It.IsAny<MovieEntity>()), Times.Once);
        }

        public static IEnumerable<object[]> CreateMovieSuccessTestData => new List<object[]>
        {
            new object[]
            {
                new MovieEntity { Title = "Sample 1", ReleaseDate = DateTime.Now, Language = "Malayalam", Actors = new List<string>{ "Actor 1", "Actor 2" } },
                new MovieEntity{ Title = "Sample 1", ReleaseDate = DateTime.Now, Language = "Malayalam", Actors = new List<string>{ "Actor 1", "Actor 2" } , Id = "RandomID" },
            }
        };
    }
}
