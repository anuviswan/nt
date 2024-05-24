using AutoMapper;
using System;
using UserService.Service.Dtos;
using UserService.Service.Query;

namespace UserService.Service.Tests.Queries;

public class SearchUserByUserNameQueryHandlerTests
{
    [Theory]
    [MemberData(nameof(SearchUserByUserNameQueryHandler_ValidUser_FindUser_TestData))]
    public async Task SearchUserByUserNameQueryHandler_ValidUser_FindUser(SearchUserByUserNameQuery request, UserProfileDto expectedResult)
    {
        #region Arrange
        var mockUserMetaRepository = new Mock<IUserMetaInformationRepository>();
        mockUserMetaRepository.Setup(x => x.GetUser(It.IsAny<string>())).Returns<string>(x => Task.FromResult<UserMetaInformation?>(new UserMetaInformation
        {
            UserName = expectedResult.UserName!,
            DisplayName = expectedResult.DisplayName,
            Bio = expectedResult.Bio,
            Id = 100
        }));

        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(x => x.Map<UserProfileDto>(It.IsAny<UserMetaInformation>())).Returns<UserMetaInformation>(x => new UserProfileDto
        {
            UserName = x.UserName,
            Bio = x.Bio,
            DisplayName = x.DisplayName,
        });

        var cts = new CancellationTokenSource();
        #endregion

        #region Act
        var queryHandler = new SearchUserByUserNameQueryHandler(mockUserMetaRepository.Object,mockMapper.Object);
        var result = await queryHandler.Handle(request,cts.Token);
        #endregion

        #region Assert
        var userInfo = result.Should().BeOfType<UserProfileDto>().Subject;
        userInfo.Should().BeEquivalentTo<UserProfileDto>(expectedResult);

        mockUserMetaRepository.Verify(x => x.GetUser(It.Is<string>((userName) => userName == request.UserName)), Times.Exactly(1));
        #endregion
    }

    public static IEnumerable<object[]> SearchUserByUserNameQueryHandler_ValidUser_FindUser_TestData => new List<object[]>
    {
        new object[]
        {
            new SearchUserByUserNameQuery { UserName = "JiaAnu"},
            new UserProfileDto { UserName = "JiaAnu", Bio = "I am Jia", DisplayName = "Jia Anu" }
        }
    };


    [Theory]
    [MemberData(nameof(SearchUserByUserNameQueryHandler_InvalidUser_FindUser_TestData))]
    public async Task SearchUserByUserNameQueryHandler_InvalidUser_FindUser(SearchUserByUserNameQuery request)
    {
        #region Arrange
        var mockUserMetaRepository = new Mock<IUserMetaInformationRepository>();
        mockUserMetaRepository.Setup(x => x.GetUser(It.IsAny<string>())).Returns<string>(x => Task.FromResult<UserMetaInformation?>(null));

        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(x => x.Map<UserProfileDto>(It.IsAny<UserMetaInformation>())).Returns<UserMetaInformation>(x => new UserProfileDto
        {
            UserName = x.UserName,
            Bio = x.Bio,
            DisplayName = x.DisplayName,
        });

        var cts = new CancellationTokenSource();
        #endregion

        #region Act
        var queryHandler = new SearchUserByUserNameQueryHandler(mockUserMetaRepository.Object, mockMapper.Object);
        var result = await queryHandler.Handle(request, cts.Token);
        #endregion

        #region Assert
        Assert.Null(result);

        mockUserMetaRepository.Verify(x => x.GetUser(It.Is<string>((userName) => userName == request.UserName)), Times.Exactly(1));
        mockMapper.Verify(x => x.Map<UserProfileDto>(It.IsAny<UserMetaInformation>()), Times.Never);
        #endregion
    }

    public static IEnumerable<object[]> SearchUserByUserNameQueryHandler_InvalidUser_FindUser_TestData => new List<object[]>
    {
        new object[]
        {
            new SearchUserByUserNameQuery { UserName = "InvalidUser"},
        }
    };


    [Theory]
    [MemberData(nameof(SearchUserByUserNameQueryHandler_NullOrEmptyUser_FindUser_TestData))]
    public async Task SearchUserByUserNameQueryHandler_NullOrEmptyUser_FindUser(SearchUserByUserNameQuery request, Type expectedException)
    {
        #region Arrange
        var mockUserMetaRepository = new Mock<IUserMetaInformationRepository>();
        mockUserMetaRepository.Setup(x => x.GetUser(It.IsAny<string>())).Returns<string>(x => Task.FromResult<UserMetaInformation?>(null));

        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(x => x.Map<UserProfileDto>(It.IsAny<UserMetaInformation>())).Returns<UserMetaInformation>(x => new UserProfileDto
        {
            UserName = x.UserName,
            Bio = x.Bio,
            DisplayName = x.DisplayName,
        });

        var cts = new CancellationTokenSource();
        #endregion

        #region Act
        var queryHandler = new SearchUserByUserNameQueryHandler(mockUserMetaRepository.Object, mockMapper.Object);
        await Assert.ThrowsAsync(expectedException, async ()=>await queryHandler.Handle(request, cts.Token));
        #endregion

    }

    public static IEnumerable<object[]> SearchUserByUserNameQueryHandler_NullOrEmptyUser_FindUser_TestData => new List<object[]>
    {
        new object[]
        {
            new SearchUserByUserNameQuery { UserName = string.Empty},
            typeof(ArgumentException)
        }
    };
}
