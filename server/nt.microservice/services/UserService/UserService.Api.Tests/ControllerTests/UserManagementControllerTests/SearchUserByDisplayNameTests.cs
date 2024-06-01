using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using UserService.Api.ViewModels.UserManagement;
using UserService.Service.Dtos;
using UserService.Service.Query;

namespace UserService.Api.Tests.ControllerTests.UserManagementControllerTests;

public class SearchUserByDisplayNameTests : ControllerTestBase
{
    #region 200 Test
    [Theory]
    [MemberData(nameof(SearchUserByDisplayName_ValidData_ShouldSucceed_TestData))]
    public async Task SearchUserByDisplayName_ValidData_ShouldSucceed(string request, SearchUserByDisplayNameResponseViewModel expectedResult)
    {
        #region Arrange
        var mockMediator = new Mock<IMediator>();
        var mockMapper = new Mock<IMapper>();

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "anuviswan"),

        }, "mock"));

        mockMediator.Setup(x => x.Send(It.IsAny<SearchUserByDisplayNameQuery>(), It.IsAny<CancellationToken>()))
                    .Returns<SearchUserByDisplayNameQuery, CancellationToken>((x, _) 
                    => Task.FromResult<IEnumerable<UserProfileDto>>
                    (
                        expectedResult
                        .Users
                        .Select(user => new UserProfileDto 
                        {
                            UserName = user.UserName,
                            DisplayName = user.DisplayName
                        })
                    ));


        mockMapper.Setup(x => x.Map<SearchUserByDisplayNameResponseViewModel>(It.IsAny<IEnumerable<UserProfileDto>>())).Returns<IEnumerable<UserProfileDto>>(x => new SearchUserByDisplayNameResponseViewModel
        {
            Users = x.Select(user => new UserProfileViewModel
            {
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                Bio = user.Bio,
                FollowersCount = user.Followers.Count(),
                IsFollowing = false,
            })
        });

        var nullLogger = CreateNullLogger<UserManagementController>();

        #endregion

        #region Act

        var sut = new UserManagementController(mockMediator.Object, mockMapper.Object, nullLogger);
        sut.ControllerContext.HttpContext = new DefaultHttpContext() { User = user };
        MockModelState(request, sut);
        var actualResult = await sut.SearchUserByDisplayName(request);
        #endregion

        #region Assert
        var okObjectResult = actualResult.Result.Should()
                           .BeOfType<OkObjectResult>()
                           .Subject;
        okObjectResult.Value.Should().BeOfType<SearchUserByDisplayNameResponseViewModel>();
        okObjectResult.Value.Should().BeEquivalentTo(expectedResult);
        mockMediator.Verify(x => x.Send(It.IsAny<SearchUserByDisplayNameQuery>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
        #endregion
    }

    public static IEnumerable<object[]> SearchUserByDisplayName_ValidData_ShouldSucceed_TestData => new List<object[]>
    {
        new object[]
        {
            "anu",
            new SearchUserByDisplayNameResponseViewModel
            {
                Users = [
                    new UserProfileViewModel
                    {
                        UserName = "jia.anu",
                        DisplayName ="Jia Anu",
                        Bio = "I am Jia Anu",
                        FollowersCount =0,
                        IsFollowing = false
                    },
                    new UserProfileViewModel
                    {
                        UserName = "niana.anu",
                        DisplayName ="Naina Anu",
                        Bio = "I am Naina Anu",
                        FollowersCount =0,
                        IsFollowing = false
                    },
                ]
            }
        }
    };

    #endregion


    #region 400 Test
    [Theory]
    [MemberData(nameof(SearchUserByDisplayName_InvalidData_ShouldFail_TestData))]
    public async Task SearchUserByDisplayName_InvalidData_ShouldFail(string request, SerializableError expectedError)
    {
        #region Arrange
        var mockMediator = new Mock<IMediator>();
        var mockMapper = new Mock<IMapper>();

        var nullLogger = CreateNullLogger<UserManagementController>();

        #endregion

        #region Act

        var userController = new UserManagementController(mockMediator.Object, mockMapper.Object, nullLogger);
        MockModelState(request, userController);
        var actualResult = await userController.SearchUserByDisplayName(request);
        #endregion

        #region Assert
        var badObjectResult = actualResult.Result
                            .Should()
                            .BeOfType<BadRequestObjectResult>()
                            .Subject;

        var error = badObjectResult.Value
            .Should()
            .BeOfType<SerializableError>()
            .Subject;

        error.Should().BeEquivalentTo(expectedError);
        //mockMediator.Verify(x => x.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        #endregion
    }

    public static IEnumerable<object[]> SearchUserByDisplayName_InvalidData_ShouldFail_TestData => new List<object[]>
    {
        new object[]
        {
            string.Empty,
            new SerializableError
            {
                [nameof(SearchUserByUserNameRequestViewModel.UserName)]= new[] {"UserName is mandatory."},
            }
        }
    };

    #endregion




}
