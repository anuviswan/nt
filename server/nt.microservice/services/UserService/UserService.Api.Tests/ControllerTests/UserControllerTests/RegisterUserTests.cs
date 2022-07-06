using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;

namespace UserService.Api.Tests.ControllerTests.UserControllerTests;

public class RegisterUserTests:ControllerTestBase
{

    #region 200 Tests
    [Theory]
    [MemberData(nameof(RegisterUser_ValidData_ShouldSucceed_TestData))]
    public async Task RegisterUser_ValidData_ShouldSucceed(CreateUserRequestViewModel request,CreateUserResponseViewModel expectedResult)
    {
        var cancellationToken = default(CancellationToken);
        var mockMediator = new Moq.Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<CreateUserCommand>(),It.IsAny<CancellationToken>()))
            .Returns<CreateUserCommand,CancellationToken>((x,_)=> Task.FromResult(new UserMetaInformation
            {
                UserName = x.UserProfile.UserName,
                DisplayName = x.UserProfile.DisplayName
            }));
        
        var mockMapper = new Moq.Mock<IMapper>();
        mockMapper.Setup(x => x.Map<UserMetaInformation>(It.IsAny<CreateUserRequestViewModel>())).Returns<CreateUserRequestViewModel>(x => new UserMetaInformation
        {
            UserName = x.UserName,
            DisplayName = x.DisplayName,
            Bio = x.Bio,
        });

        mockMapper.Setup(x => x.Map<CreateUserResponseViewModel>(It.IsAny<UserMetaInformation>())).Returns<UserMetaInformation>(x => new CreateUserResponseViewModel
        {
            UserName = x.UserName,
        });

        var nullLogger = CreateNullLogger<UserController>();

        var userController = new UserController(mockMediator.Object, mockMapper.Object, nullLogger);
        MockModelState(request, userController);
        var actualResult = await userController.RegisterUser(request);

        var okObjectResult  = actualResult.Result.Should()
                           .BeOfType<OkObjectResult>()
                           .Subject;
        okObjectResult.Value.Should().BeOfType<CreateUserResponseViewModel>();
        okObjectResult.Value.Should().BeEquivalentTo(expectedResult);
        mockMediator.Verify(x => x.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
    }

    public static IEnumerable<object[]> RegisterUser_ValidData_ShouldSucceed_TestData => new List<object[]>
    {
        new object[]
        {
            new CreateUserRequestViewModel { UserName = "SampleUser"},
            new CreateUserResponseViewModel { UserName = "SampleUser" }
        }
    };

    #endregion

    #region 400 Bad Request Tests

    [Theory]
    [MemberData(nameof(RegisterUser_ValidData_ShouldFail_TestData))]
    public async Task RegisterUser_ValidData_ShouldFail(CreateUserRequestViewModel request, SerializableError expectedError)
    {
        #region Arrange
        var mockMediator = new Moq.Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
            .Returns<CreateUserCommand, CancellationToken>((x, _) => Task.FromResult(new UserMetaInformation
            {
                UserName = x.UserProfile.UserName,
                DisplayName = x.UserProfile.DisplayName
            }));

        var mockMapper = new Moq.Mock<IMapper>();
        mockMapper.Setup(x => x.Map<UserMetaInformation>(It.IsAny<CreateUserRequestViewModel>())).Returns<CreateUserRequestViewModel>(x => new UserMetaInformation
        {
            UserName = x.UserName,
            DisplayName = x.DisplayName,
            Bio = x.Bio,
        });

        mockMapper.Setup(x => x.Map<CreateUserResponseViewModel>(It.IsAny<UserMetaInformation>())).Returns<UserMetaInformation>(x => new CreateUserResponseViewModel
        {
            UserName = x.UserName,
        });

        var nullLogger = CreateNullLogger<UserController>();
        #endregion


        #region Act

        var userController = new UserController(mockMediator.Object, mockMapper.Object, nullLogger);
        MockModelState(request, userController);
        var actualResult = await userController.RegisterUser(request);

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
        mockMediator.Verify(x => x.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        
        #endregion
    }

    public static IEnumerable<object[]> RegisterUser_ValidData_ShouldFail_TestData => new List<object[]>
    {
        #region Required Fields
        new object[]
        {
            new CreateUserRequestViewModel { UserName = string.Empty },
            new SerializableError
            {
                [nameof(CreateUserRequestViewModel.UserName)]= new[] {"UserName is mandatory."},
            }
        },
       
        #endregion

        #region Minimum Fields
        new object[]
        {
            new CreateUserRequestViewModel { UserName = "12345"},
            new SerializableError
            {
                [nameof(CreateUserRequestViewModel.UserName)]= new[] {"UserName should be minimum 6 characters."},
            }
        },

        #endregion

        #region Maximum Fields
        new object[]
        {
            new CreateUserRequestViewModel { UserName = "1234567890123456"},
            new SerializableError
            {
                [nameof(CreateUserRequestViewModel.UserName)]= new[] {"UserName should be maximum 15 characters."},
            }
        },
        #endregion
    };

    #endregion
}