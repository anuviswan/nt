using MovieService.Api.ViewModels;
using MovieService.Data.Interfaces.Entities;
using Omu.ValueInjecter;

namespace MovieService.Api.Tests.MapperTests;

[TestClass]
public class ValueInjecterTests
{
    [TestMethod]
    public void TestMapViewModelToEntity()
    {
        // Arrange
        var movieRequest = new CreateMovieRequest
        {
            Title = "Title",
            MovieLanguage = "en",
            ReleaseDate = new DateTime(2001, 1, 1)
        };

        // Act
        var movieEntity = Mapper.Map<MovieEntity>(movieRequest);

        // Assert
        Assert.AreEqual(movieRequest.Title, movieEntity.Title);
        Assert.AreEqual(movieRequest.MovieLanguage, movieEntity.MovieLanguage);
        Assert.AreEqual(movieRequest.ReleaseDate, movieEntity.ReleaseDate);
    }
}
