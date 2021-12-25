using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Api.Tests.ControllerTests.UserControllerTests;
public class ChangePasswordTests:ControllerTestBase
{
    [Theory]
    [MemberData(nameof(ChangePassword_ValidData_ShouldSucceed_TestData))]
    public async Task ChangePassword_ValidData_Success(ChangePasswordRequestViewModel request,ChangePasswordResponseViewModel expectedResult)
    {
        var mockMediator = new Mock<IMediator>();

        mockMediator.Setup(x => x.Send(It.IsAny<ChangePasswordCommand>(),It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(true));

        var mockMapper = new Moq.Mock<IMapper>();
        mockMapper.Setup(x => x.Map<User>(It.IsAny<ChangePasswordRequestViewModel>())).Returns<ChangePasswordRequestViewModel>(x => new User
        {
            UserName = x.UserName,
            Password = x.Password
        });

        var userController = new UserController(mockMediator.Object, mockMapper.Object);
        MockModelState(request, userController);
        var actualResult = await userController.ChangePassword(request);

        var okObjectResult = actualResult.Result.Should()
                           .BeOfType<OkObjectResult>()
                           .Subject;
        okObjectResult.Value.Should().BeOfType<ChangePasswordResponseViewModel>();
        okObjectResult.Value.Should().BeEquivalentTo(expectedResult);
        mockMediator.Verify(x => x.Send(It.IsAny<ChangePasswordCommand>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
    }

    public static IEnumerable<object[]> ChangePassword_ValidData_ShouldSucceed_TestData => new List<object[]>
    {
        new object[]
        {
            new ChangePasswordRequestViewModel { UserName = "SampleUser", Password = "Dummy1234" },
            new ChangePasswordResponseViewModel { UserName = "SampleUser", IsPasswordChanged = true}
        }
    };
}
