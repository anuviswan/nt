using Moq;
using Nt.Application.Services.Movie;
using Nt.Domain.Entities.Movie;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.RepositoryContracts.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        protected override void InitializeCollection()
        {
            base.InitializeCollection();
            EntityCollection = new List<MovieEntity>
            {
                new MovieEntity { Title = "Movie Sample 1",Language = "Malayalam", ReleaseDate = DateTime.Now },
                new MovieEntity { Title = "Movie Sample 2",Language = "Malayalam", ReleaseDate = DateTime.Now },
                new MovieEntity { Title = "Movie Sample 3",Language = "Malayalam", ReleaseDate = DateTime.Now },
            };
        }

        [Theory]
        [MemberData(nameof(CreateMovieSuccessTestData))]
        public async Task CreateMovieSuccessTest(MovieEntity request,MovieEntity expected)
        {
            var mockMovieRepository = new Mock<IMovieRepository>();
            mockMovieRepository.Setup(x => x.CreateAsync(It.IsAny<MovieEntity>())).Returns(Task.FromResult(expected));
            mockMovieRepository.Setup(x=>x.GetAsync(It.IsAny<Expression<Func<MovieEntity, bool>>>()))
                .Returns(Task.FromResult(EntityCollection.Where(x => x.Title.ToLower().Equals(request.Title.ToLower())
            && x.ReleaseDate.Year == request.ReleaseDate.Year
            && x.Language.ToLower().Equals(request.Language.ToLower()))));
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(x => x.MovieRepository).Returns(mockMovieRepository.Object);
            var movieService = new MovieService(mockUnitOfWork.Object);
            var result = await movieService.CreateAsync(request);


            Assert.False(string.IsNullOrEmpty(result.Id));
            Assert.Equal(expected.Title, request.Title);
            mockMovieRepository.Verify(x => x.CreateAsync(It.IsAny<MovieEntity>()), Times.Once);
            mockMovieRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<MovieEntity, bool>>>()), Times.Once);
        }

        public static IEnumerable<object[]> CreateMovieSuccessTestData => new List<object[]>
        {
            new []
            {
                 new MovieEntity { Title = "Movie Sample 1", ReleaseDate = DateTime.Now, Language = "English", Actors = new List<string>{ "Actor 1", "Actor 2" } },
                 new MovieEntity { Title = "Movie Sample 1", ReleaseDate = DateTime.Now, Language = "english", Actors = new List<string>{ "Actor 1", "Actor 2" }, Id = "RandomId" }
               
            },
            new[]
            {
                new MovieEntity { Title = "Movie Sample 1", ReleaseDate = DateTime.Now, Language = "english", Actors = new List<string>{ "Actor 1", "Actor 2" } },
                new MovieEntity { Title = "Movie Sample 1", ReleaseDate = DateTime.Now, Language = "english", Actors = new List<string>{ "Actor 1", "Actor 2" } , Id = "RandomId"}
            },
            new[]
            {
                new MovieEntity { Title = "Movie Sample 1", ReleaseDate = DateTime.Now.AddYears(-1), Language = "Malayalam", Actors = new List<string>{ "Actor 1", "Actor 2" } },
                new MovieEntity { Title = "Movie Sample 1", ReleaseDate = DateTime.Now.AddYears(-1), Language = "malayalam", Actors = new List<string>{ "Actor 1", "Actor 2" } , Id = "RandomId"}
            },
        };
    }
}
