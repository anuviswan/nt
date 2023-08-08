using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UserService.Api.ViewModels.UserManagement;
using UserService.Service.Dtos;
using UserService.Service.Query;
using Xunit.Sdk;

namespace UserService.Api.Tests.ControllerTests.UserManagementControllerTests;

public class SearchUserByUserNameTests : ControllerTestBase
{
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
                        UserName = expectedResult.User.DisplayName,
                        DisplayName = expectedResult.User.DisplayName,
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
                Followers = x.Followers.Count(),
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
                    Followers =0,
                    IsFollowing = false
                }
            }
        }
    };
}
