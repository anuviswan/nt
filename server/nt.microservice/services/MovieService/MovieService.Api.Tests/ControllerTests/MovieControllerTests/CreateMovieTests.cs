using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using MovieService.Api.Controllers;
using MovieService.Api.ViewModels;
using MovieService.Service.Interfaces.Dtos;
using MovieService.Service.Interfaces.Services;

namespace MovieService.Api.Tests.ControllerTests.MovieControllerTests;

[TestClass]
public class CreateMovieTests : ControllerTestsBase
{
    [DataTestMethod]
    [DynamicData(nameof(CreateMovie_ValidMovieTitle_Success_DataSource), DynamicDataSourceType.Property)]
    public async Task CreateMovie_ValidMovieTitle_Success(CreateMovieRequest createMovieRequest, CreateMovieResponse expectedResults)
    {
        #region Arrange
        var mockMovieService = A.Fake<IMovieService>();
        A.CallTo(() => mockMovieService.CreateMovie(A<MovieDto>.Ignored)).ReturnsLazily( (MovieDto movie) => Task.FromResult(new MovieDto
        {
            Title = movie.Title,
            MovieLanguage = movie.MovieLanguage,
            ReleaseDate = new DateTime(2016, 1, 1)
        }));

        var nullLogger = CreateNullLogger<MovieController>();
        #endregion

        #region Act
        var sut = new MovieController(mockMovieService, nullLogger);
        var response = await sut.CreateMovie(createMovieRequest).ConfigureAwait(false);
        #endregion

        #region Assert
        Assert.IsNotNull(response);
        Assert.IsTrue(response.Result is OkObjectResult);
        #endregion
    }

    public static IEnumerable<object[]> CreateMovie_ValidMovieTitle_Success_DataSource
    {
        get
        {
            yield return new object[]
             {
                new CreateMovieRequest
                {
                    Title = "Inception",
                    Language = "English",
                    ReleaseDate = new DateTime(2010, 7, 16)
                },
                new CreateMovieResponse
                {
                    
                }
             };
        }
    }
}
