using MovieService.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using MovieService.Service.Services;
using MovieService.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MovieService.Api.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

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
        A.CallTo(() => mockMovieService.CreateMovie(A<Domain.Entities.Movie>.Ignored)).ReturnsLazily( (Movie movie) => Task.FromResult(new Movie
        {
            Title = movie.Title,
            Language = movie.Language,
            ReleaseDate = new DateOnly(2016, 1, 1)
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
                    ReleaseDate = new DateOnly(2010, 7, 16)
                },
                new CreateMovieResponse
                {
                    
                }
             };
        }
    }
}
