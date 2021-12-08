using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.User;
using Nt.Domain.ServiceContracts.User;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.GetAllUser;

namespace Nt.Infrastructure.Tests.Controllers.UserControllerTests;
public class GetUserTests : ControllerTestBase<UserProfileEntity>
{
    public GetUserTests(ITestOutputHelper testOutput) : base(testOutput)
    {

    }

    protected override void InitializeCollection()
    {
        Output.WriteLine("Initialized"); //TODO: Fix this removing this line causes InitializeCollection not being called randomly
        EntityCollection = Enumerable.Range(1, 10).Select(x => new UserProfileEntity
        {
            DisplayName = $"User Name {x}",
            UserName = $"username{x}",
            PassKey = $"passKey{x}"
        }).ToList();

        EntityCollection.AddRange(new[]
        {
            new UserProfileEntity{ UserName = "jiaanu", DisplayName="nainaanu" },
            new UserProfileEntity{ UserName = "nainaanu", DisplayName="jiaanu" }
        });
    }

    #region ResponseStatus 200
    [Theory]
    [MemberData(nameof(GetUser_ResponseStatus_200_TestData))]
    [ControllerTest(nameof(UserController)), Feature]
    public async Task GetUser_ResponseStatus_200(string userName, UserProfileResponse expectedOutput)
    {
        // Arrange
        var mockUserManagementService = new Mock<IUserManagementService>();
        mockUserManagementService.Setup(x => x.GetUserAsync(userName))
            .Returns(Task.FromResult(result: EntityCollection.Single(x => x.UserName.StartsWith(userName))));
        var userController = new UserController(Mapper, null, mockUserManagementService.Object, null);

        // Act
        var response = await userController.GetUser(userName);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(response.Result);
        var result = Assert.IsType<UserProfileResponse>(okObjectResult.Value);
        Assert.Equal(expectedOutput.UserName, result.UserName);
        Assert.Equal(expectedOutput.DisplayName, result.DisplayName);
    }
    public static IEnumerable<object[]> GetUser_ResponseStatus_200_TestData => new []
    {
        new object[]
        {
            "username2", 
            new UserProfileResponse{ UserName="username2",DisplayName="User Name 2" } 
        },
        new object[]
        {
            "jiaanu", 
            new UserProfileResponse{ UserName="jiaanu",DisplayName="nainaanu" } 
        },
    };
    #endregion

    #region ResponseStatus 400
    [Theory]
    [MemberData(nameof(GetUser_ResponseStatus_400_TestData))]
    [ControllerTest(nameof(UserController)), Feature]
    public async Task GetUser_ResponseStatus_400(string userName)
    {
        // Arrange
        var mockUserManagementService = new Mock<IUserManagementService>();
        mockUserManagementService.Setup(x => x.GetUserAsync(userName)).Throws<EntityNotFoundException>();
        var userController = new UserController(Mapper, null, mockUserManagementService.Object, null);

        // Act
        var response = await userController.GetUser(userName);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
    }
    public static IEnumerable<object[]> GetUser_ResponseStatus_400_TestData => new []
    {
        new object[]{"username12"},
    };
    #endregion

    #region Invalid Cases
    [Theory]
    [MemberData(nameof(GetUser_InvalidCasesTestData))]
    [ControllerTest(nameof(UserController)), Feature]
    public async Task GetUser_InvalidCases(string userName)
    {
        // Arrange
        var mockUserManagementService = new Mock<IUserManagementService>();
        mockUserManagementService.Setup(x => x.GetUserAsync(userName)).Throws<Exception>();
        var userController = new UserController(Mapper, null, mockUserManagementService.Object, null);

        // Act
        // Assert
        await Assert.ThrowsAsync<Exception>(() => userController.GetUser(userName));
    }
    public static IEnumerable<object[]> GetUser_InvalidCasesTestData => new []
    {
        new object[]{"username1" },
    };
    #endregion


}
