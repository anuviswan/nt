using AutoMapper;
using System;
using System.Linq;
using UserService.Service.Dtos;
using UserService.Service.Query;

namespace UserService.Service.Tests.Queries;

public class SearchUserByDisplayNameQueryHandlerTests
{
    [Theory]
    [MemberData(nameof(SearchUserByDisplayNameQueryHandler_ValidUser_FindUser_TestData))]
    public async Task SearchUserByDisplayNameQueryHandler_ValidUser_FindUser(SearchUserByDisplayNameQuery request, IEnumerable<UserProfileDto> expectedResult)
    {
        #region Arrange
        var mockUserMetaRepository = new Mock<IUserMetaInformationRepository>();
        mockUserMetaRepository.Setup(x => x.SearchUserByPartialDisplayName(It.IsAny<string>()))
            .Returns<string>(x => 
            Task.FromResult<IEnumerable<UserMetaInformation>>(
                    expectedResult.Select(user => new UserMetaInformation
                    {
                        UserName = user.UserName!,
                        DisplayName = user.DisplayName,
                        Bio = user.Bio,
                        Id = 100
                    })
                ));

        var mockMapper = new Mock<IMapper>();

        mockMapper.Setup(x => x.Map<IEnumerable<UserProfileDto>>(It.IsAny<IEnumerable<UserMetaInformation>>()))
            .Returns<IEnumerable<UserMetaInformation>>(src => src.Select(user =>
                            new UserProfileDto
                            {
                                UserName = user.UserName,
                                Bio = user.Bio,
                                DisplayName = user.DisplayName
                            }
        ));

        var cts = new CancellationTokenSource();
        #endregion

        #region Act
        var sut = new SearchUserByDisplayNameQueryHandler(mockUserMetaRepository.Object, mockMapper.Object);
        var result = await sut.Handle(request, cts.Token);
        #endregion

        #region Assert
        var users = result.Should().BeAssignableTo<IEnumerable<UserProfileDto>>().Subject;
        users.Should().BeEquivalentTo<UserProfileDto>(expectedResult);

        mockUserMetaRepository.Verify(x => x.SearchUserByPartialDisplayName(It.Is<string>((userName) => userName == request.QueryPart)), Times.Exactly(1));
        #endregion
    }

    public static IEnumerable<object[]> SearchUserByDisplayNameQueryHandler_ValidUser_FindUser_TestData => new List<object[]>
    {
        new object[]
        {
            new SearchUserByDisplayNameQuery { QueryPart="Jia", CurrentUserName = "naina.anu"},
            new UserProfileDto[] {new UserProfileDto { UserName = "JiaAnu", Bio = "I am Jia", DisplayName = "Jia Anu" } },
        }
    };


    [Theory]
    [MemberData(nameof(SearchUserByDisplayNameQueryHandler_InvalidUser_FindUser_TestData))]
    public async Task SearchUserByDisplayNameQueryHandler_InvalidUser_FindUser(SearchUserByDisplayNameQuery request)
    {
        #region Arrange
        var mockUserMetaRepository = new Mock<IUserMetaInformationRepository>();
        mockUserMetaRepository.Setup(x => x.SearchUserByPartialDisplayName(It.IsAny<string>()))
                        .Returns<string>(x => Task.FromResult<IEnumerable<UserMetaInformation>>([]));

        var mockMapper = new Mock<IMapper>();

        mockMapper.Setup(x => x.Map<IEnumerable<UserProfileDto>>(It.IsAny<IEnumerable<UserMetaInformation>>()))
            .Returns<IEnumerable<UserMetaInformation>>(src => src.Select(user =>
                            new UserProfileDto
                            {
                                UserName = user.UserName,
                                Bio = user.Bio,
                                DisplayName = user.DisplayName
                            }
        )); 

        var cts = new CancellationTokenSource();
        #endregion

        #region Act
        var sut = new SearchUserByDisplayNameQueryHandler(mockUserMetaRepository.Object, mockMapper.Object);
        var result = await sut.Handle(request, cts.Token);
        #endregion

        #region Assert
        result.Should().BeEmpty();
        mockUserMetaRepository.Verify(x => x.SearchUserByPartialDisplayName(It.Is<string>((userName) => userName == request.QueryPart)), Times.Exactly(1));
        mockMapper.Verify(x => x.Map<IEnumerable<UserProfileDto>>(It.IsAny<IEnumerable<UserMetaInformation>>()), Times.Never);
        #endregion
    }

    public static IEnumerable<object[]> SearchUserByDisplayNameQueryHandler_InvalidUser_FindUser_TestData => new List<object[]>
    {
        new object[]
        {
            new SearchUserByDisplayNameQuery { QueryPart = "InvalidUser", CurrentUserName = "naina.anu"},
        }
    };


    [Theory]
    [MemberData(nameof(SearchUserByDisplayNameQueryHandler_NullOrEmptyUser_FindUser_TestData))]
    public async Task SearchUserByDisplayNameQueryHandler_NullOrEmptyUser_FindUser(SearchUserByDisplayNameQuery request, Type expectedException)
    {
        #region Arrange
        var mockUserMetaRepository = new Mock<IUserMetaInformationRepository>();
        mockUserMetaRepository.Setup(x => x.SearchUserByPartialDisplayName(It.IsAny<string>()))
                        .Returns<string>(x => Task.FromResult<IEnumerable<UserMetaInformation>>([]));

        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(x => x.Map<IEnumerable<UserProfileDto>>(It.IsAny<IEnumerable<UserMetaInformation>>()))
            .Returns<IEnumerable<UserMetaInformation>>(src => src.Select(user =>
                            new UserProfileDto
                            {
                                UserName = user.UserName,
                                Bio = user.Bio,
                                DisplayName = user.DisplayName
                            }
        ));

        var cts = new CancellationTokenSource();
        #endregion

        #region Act
        var queryHandler = new SearchUserByDisplayNameQueryHandler(mockUserMetaRepository.Object, mockMapper.Object);
        await Assert.ThrowsAsync(expectedException, async () => await queryHandler.Handle(request, cts.Token));
        #endregion

    }

    public static IEnumerable<object[]> SearchUserByDisplayNameQueryHandler_NullOrEmptyUser_FindUser_TestData => new List<object[]>
    {
        new object[]
        {
            new SearchUserByDisplayNameQuery { QueryPart = string.Empty, CurrentUserName = "naina.anu"},
            typeof(ArgumentException)
        }
    };
}
