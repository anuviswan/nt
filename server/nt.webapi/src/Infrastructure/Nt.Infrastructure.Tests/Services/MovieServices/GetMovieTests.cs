﻿using Nt.Application.Services.Movie;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.Movie;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.RepositoryContracts.Movie;
using Nt.Domain.RepositoryContracts.User;
using Nt.Infrastructure.Tests.Helpers;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;
using Nt.Infrastructure.Tests.Helpers.TestData;
using System.Linq.Expressions;

namespace Nt.Infrastructure.Tests.Services.MovieServices;
public class GetMovieTests:ServiceTestBase<MovieEntity>
{
    public static List<UserProfileEntity> UserCollection { get; set; } = MockDataHelper.UserCollection;
    public static List<ReviewEntity> ReviewCollection { get; set; } = MockDataHelper.ReviewCollection;
    public static List<MovieEntity> MovieCollection { get; set; } = MockDataHelper.MovieCollection;

    public GetMovieTests(ITestOutputHelper output) : base(output)
    {
    }

    protected override void InitializeCollection()
    {
        base.InitializeCollection();
        EntityCollection = new (MovieCollection);
    }


    [Theory]
    [ServiceTest(nameof(MovieService)), Feature]
    [MemberData(nameof(GetMovieSuccessTestData))]
    public async Task GetMovieSuccessTest(string movieId,MovieEntity expectedResult)
    {
        // Arrange
        var mockMovieRepository = new Mock<IMovieRepository>();
        mockMovieRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<MovieEntity, bool>>>()))
                            .Returns(Task.FromResult(EntityCollection.Where(x => x.Id == movieId)));

        var mockReviewRepository = new Mock<IReviewRepository>();
        mockReviewRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ReviewEntity, bool>>>()))
                            .Returns(Task.FromResult(ReviewCollection.Where(x => x.MovieId == movieId)));

        var mockUserRepository = new Mock<IUserProfileRepository>();
        mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
                            .Returns((Expression<Func<UserProfileEntity, bool>> x) => 
                            Task.FromResult(UserCollection.AsQueryable<UserProfileEntity>().Where(x).AsEnumerable()));

        var unitOfWork = new Mock<IUnitOfWork>();
        unitOfWork.SetupGet(x => x.MovieRepository).Returns(mockMovieRepository.Object);
        unitOfWork.SetupGet(x => x.UserProfileRepository).Returns(mockUserRepository.Object);
        unitOfWork.SetupGet(x => x.ReviewRepository).Returns(mockReviewRepository.Object);

        // Act
        var movieService = new MovieService(unitOfWork.Object);
        var result = await movieService.GetOne(movieId);

        // Assert
        Assert.Equal(expectedResult.Id, result.Id);
        Assert.Equal(expectedResult.Title, result.Title);
        Assert.Equal(expectedResult.PlotSummary, result.PlotSummary);
        Assert.Equal(expectedResult.Director, result.Director);
        Assert.Equal(expectedResult.CastAndCrew, result.CastAndCrew);
        Assert.Equal(expectedResult.ReleaseDate, result.ReleaseDate);
        Assert.Equal(expectedResult.Language, result.Language);
        Assert.Equal(expectedResult.TotalReviews, result.TotalReviews);
        Assert.Equal(expectedResult.Rating, result.Rating);
    }

    public static IEnumerable<object[]> GetMovieSuccessTestData => new List<object[]>
    {
        new object[]
        {
            string.Format(Utils.MockMovieIdFormat,1),
            MockDataHelper.GetMovie(string.Format(Utils.MockMovieIdFormat,1))
        },
        new object[]
        {
            string.Format(Utils.MockMovieIdFormat,3),
            MockDataHelper.GetMovie(string.Format(Utils.MockMovieIdFormat,3))
        }
    };

    [Theory]
    [ServiceTest(nameof(MovieService)), Feature]
    [MemberData(nameof(GetMovieFailureTestData))]
    public async Task GetMovieFailureTest(string movieId,Exception exception)
    {
        var mockMovieRepository = new Mock<IMovieRepository>();
        mockMovieRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<MovieEntity, bool>>>()))
                            .Returns(Task.FromResult(EntityCollection.Where(x => x.Id == movieId)));

        var mockReviewRepository = new Mock<IReviewRepository>();
        mockReviewRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ReviewEntity, bool>>>()))
                            .Returns(Task.FromResult(ReviewCollection.Where(x => x.MovieId == movieId)));

        var mockUserRepository = new Mock<IUserProfileRepository>();
        mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
                            .Returns((Expression<Func<UserProfileEntity, bool>> x) =>
                            Task.FromResult(UserCollection.AsQueryable<UserProfileEntity>().Where(x).AsEnumerable()));


        var unitOfWork = new Mock<IUnitOfWork>();
        unitOfWork.SetupGet(x => x.MovieRepository).Returns(mockMovieRepository.Object);
        unitOfWork.SetupGet(x => x.UserProfileRepository).Returns(mockUserRepository.Object);
        unitOfWork.SetupGet(x => x.ReviewRepository).Returns(mockReviewRepository.Object);

        // Act
        var movieService = new MovieService(unitOfWork.Object);
        await Assert.ThrowsAsync(exception.GetType(), () => movieService.GetOne(movieId));
    }


    public static IEnumerable<object[]> GetMovieFailureTestData => new []
    {
        new object[]
        {
            "M4",
            new EntityNotFoundException()
        },
            
    };
}
