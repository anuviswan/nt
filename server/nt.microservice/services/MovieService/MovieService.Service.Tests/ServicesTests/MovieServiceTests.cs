using FakeItEasy;
using MovieService.Data.Interfaces.Services;
using MovieService.Domain.Entities;

namespace MovieService.Service.Tests.ServicesTests;

[TestClass]
public class MovieServiceTests : ServiceTestsBase
{
    [TestMethod]
    [DynamicData(nameof(CreateMovie_ValidMovieTitle_Success_DataSource), DynamicDataSourceType.Property)]
    public async Task CreateMovie_ValidParameters_Success(Movie movie, Movie expectedResult)
    {
        #region Arrange
        var movieCrudService = A.Fake<IMovieCrudService>();
        A.CallTo(() => movieCrudService.CreateAsync(A<Movie>.Ignored)).ReturnsLazily((Movie entity) => Task.FromResult(entity));
        var nullLogger = CreateNullLogger<Services.MovieService>();

        #endregion

        #region Act
        var sut = new Services.MovieService(movieCrudService, nullLogger);
        var result = await sut.CreateMovie(movie);
        #endregion

        #region Assert
        Assert.AreEqual(expectedResult.Title, result.Title);
        Assert.AreEqual(expectedResult.Language, result.Language);
        Assert.AreEqual(expectedResult.ReleaseDate, result.ReleaseDate);
        #endregion
    }

    public static IEnumerable<object[]> CreateMovie_ValidMovieTitle_Success_DataSource
    {
        get
        {
            yield return new object[]
             {
                new Movie
                {
                    Title = "Inception",
                    Language = "English",
                    ReleaseDate = new DateTime(2010, 7, 16)
                },
                new Movie
                {
                    Title = "Inception",
                    Language = "English",
                    ReleaseDate = new DateTime(2010, 7, 16)
                }
             };
        }
    }
}
