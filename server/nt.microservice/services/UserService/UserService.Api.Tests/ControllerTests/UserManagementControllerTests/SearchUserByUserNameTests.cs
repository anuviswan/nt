using UserService.Api.ViewModels.UserManagement;
using UserService.Service.Dtos;
using UserService.Service.Query;

namespace UserService.Api.Tests.ControllerTests.UserManagementControllerTests;

public class SearchUserByUserNameTests : ControllerTestBase
{
    #region 200 Test
    [Theory]
    [MemberData(nameof(SearchUserByUserName_ValidData_ShouldSucceed_TestData))]
    public async Task SearchUserByUserName_ValidData_ShouldSucceed(SearchUserByUserNameRequestViewModel request, SearchUserByUserNameResponseViewModel expectedResult)
    {
        #region Arrange
        var cancellationToken = default(CancellationToken);
        var mockMediator = new Mock<IMediator>();
        var mockMapper = new Mock<IMapper>();

        mockMediator.Setup(x => x.Send(It.IsAny<SearchUserByUserNameQuery>(), It.IsAny<CancellationToken>()))
                    .Returns<SearchUserByUserNameQuery, CancellationToken>((x, _) => Task.FromResult(new UserProfileDto
                    {
                        UserName = expectedResult.User.UserName,
                        DisplayName = expectedResult.User.DisplayName,
                        Bio = expectedResult.User.Bio,
                        FollowedBy = default,
                        Followers = default
                    }));


        mockMapper.Setup(x => x.Map<SearchUserByUserNameQuery>(It.IsAny<SearchUserByUserNameRequestViewModel>()))
            .Returns<SearchUserByUserNameRequestViewModel>(x => new SearchUserByUserNameQuery
            {
                UserName = x.UserName,
            });

        mockMapper.Setup(x => x.Map<SearchUserByUserNameResponseViewModel>(It.IsAny<UserProfileDto>())).Returns<UserProfileDto>(x => new SearchUserByUserNameResponseViewModel
        {
            User = new UserProfileViewModel
            {
                UserName = x.UserName,
                DisplayName = x.DisplayName,
                Bio = x.Bio,
                FollowersCount = 0,
                IsFollowing = false
            }
        });

        var nullLogger = CreateNullLogger<UserManagementController>();

        #endregion

        #region Act

        var userController = new UserManagementController(mockMediator.Object, mockMapper.Object, nullLogger);
        MockModelState(request, userController);
        var actualResult = await userController.SearchUserByUserName(request);
        #endregion

        #region Assert
        var okObjectResult = actualResult.Result.Should()
                           .BeOfType<OkObjectResult>()
                           .Subject;
        okObjectResult.Value.Should().BeOfType<SearchUserByUserNameResponseViewModel>();
        okObjectResult.Value.Should().BeEquivalentTo(expectedResult);
        mockMediator.Verify(x => x.Send(It.IsAny<SearchUserByUserNameQuery>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
        #endregion
    }

    public static IEnumerable<object[]> SearchUserByUserName_ValidData_ShouldSucceed_TestData => new List<object[]>
    {
        new object[]
        {
            new SearchUserByUserNameRequestViewModel { UserName = "SampleUser"},
            new SearchUserByUserNameResponseViewModel
            {
                User = new UserProfileViewModel
                {
                    UserName = "SearchUser",
                    DisplayName ="Search User",
                    Bio = "I am Search User",
                    FollowersCount =0,
                    IsFollowing = false
                }
            }
        }
    };

    #endregion


    #region 400 Test
    [Theory]
    [MemberData(nameof(SearchUserByUserName_InvalidData_ShouldFail_TestData))]
    public async Task SearchUserByUserName_InvalidData_ShouldFail(SearchUserByUserNameRequestViewModel request,  SerializableError expectedError)
    {
        #region Arrange
        var cancellationToken = default(CancellationToken);
        var mockMediator = new Mock<IMediator>();
        var mockMapper = new Mock<IMapper>();

        var nullLogger = CreateNullLogger<UserManagementController>();

        #endregion

        #region Act

        var userController = new UserManagementController(mockMediator.Object, mockMapper.Object, nullLogger);
        MockModelState(request, userController);
        var actualResult = await userController.SearchUserByUserName(request);
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

    public static IEnumerable<object[]> SearchUserByUserName_InvalidData_ShouldFail_TestData => new List<object[]>
    {
        new object[]
        {
            new SearchUserByUserNameRequestViewModel { UserName = string.Empty },
            new SerializableError
            {
                [nameof(SearchUserByUserNameRequestViewModel.UserName)]= new[] {"UserName is mandatory."},
            }
        }
    };

    #endregion




}
