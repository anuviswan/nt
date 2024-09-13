using MovieService.Api.ViewModels;
using MovieService.Domain.Entities;
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
            Language = "en",
            ReleaseDate = new DateTime(2001, 1, 1)
        };

        // Act
        var movieEntity = Mapper.Map<Movie>(movieRequest);

        // Assert
        Assert.AreEqual(movieRequest.Title, movieEntity.Title);
        Assert.AreEqual(movieRequest.Language, movieEntity.Language);
        Assert.AreEqual(movieRequest.ReleaseDate, movieEntity.ReleaseDate);
    }
}
