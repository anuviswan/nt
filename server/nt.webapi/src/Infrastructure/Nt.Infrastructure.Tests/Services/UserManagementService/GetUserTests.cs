using Nt.Application.Services.User;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.RepositoryContracts.User;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;
using System.Linq.Expressions;

namespace Nt.Infrastructure.Tests.Services.UserManagementServiceTests;
public class GetUserTests : ServiceTestBase<UserProfileEntity>
{
    public GetUserTests(ITestOutputHelper output) : base(output)
    {
    }
    protected override void InitializeCollection()
    {
        Output.WriteLine("Initialized"); //TODO: Fix this removing this line causes InitializeCollection not being called randomly
        EntityCollection = new()
        {
            new () { UserName = "AnuViswan", DisplayName = "Anu Viswan", IsDeleted = false },
            new () { UserName = "ManuViswan", DisplayName = "Manu Viswan", IsDeleted = false },
            new () { UserName = "ManuViswan", DisplayName = "Manu Viswan", IsDeleted = false },
            new () { UserName = "jiaanu", DisplayName = "anuviswan", IsDeleted = false },
            new () { UserName = "AnuViswan", DisplayName = "AnuViswan", IsDeleted = true },
        };
    }

    #region Valid Cases
    [Theory]
    [MemberData(nameof(SearchUserTestData))]
    [ServiceTest(nameof(UserManagementService)), Feature]
    public async Task GetUser(string userName, UserProfileEntity expectedOutput)
    {
        // Arrange
        var mockUserProfileRepository = new Mock<IUserProfileRepository>();
        mockUserProfileRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
                                    .Returns(Task.FromResult(EntityCollection.Where(x => (x.DisplayName.ToLower().StartsWith(userName.ToLower())
                                                                                            || x.UserName.ToLower().StartsWith(userName.ToLower()))
                                                                                            && x.IsDeleted == false)));
        var mockUnitOfwork = new Mock<IUnitOfWork>();
        mockUnitOfwork.SetupGet(x => x.UserProfileRepository).Returns(mockUserProfileRepository.Object);

        var userManagementService = new UserManagementService(mockUnitOfwork.Object);

        // Act
        var result = await userManagementService.GetUserAsync(userName);

        // Assert
        Assert.Equal(expectedOutput.UserName, result.UserName);
    }
    public static IEnumerable<object[]> SearchUserTestData => new []
    {
        new object[]{"AnuViswan", new UserProfileEntity{UserName="AnuViswan" } },

    };
    #endregion


    #region Invalid Cases
    [Theory]
    [MemberData(nameof(SearchUserInvalidCaseTestData))]
    [ServiceTest(nameof(UserManagementService)), Feature]
    public async Task GetUser_InvalidCase(string userName,Type expectedException)
    {
        // Arrange
        var mockUserProfileRepository = new Mock<IUserProfileRepository>();
        mockUserProfileRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
                                    .Returns(Task.FromResult(EntityCollection.Where(x => (x.DisplayName.ToLower().StartsWith(userName.ToLower())
                                                                                            || x.UserName.ToLower().StartsWith(userName.ToLower()))
                                                                                            && x.IsDeleted == false)));
        var mockUnitOfwork = new Mock<IUnitOfWork>();
        mockUnitOfwork.SetupGet(x => x.UserProfileRepository).Returns(mockUserProfileRepository.Object);

        var userManagementService = new UserManagementService(mockUnitOfwork.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync(expectedException,() => userManagementService.GetUserAsync(userName));
    }
    public static IEnumerable<object[]> SearchUserInvalidCaseTestData => new []
    {
        new object[]{"Anu",typeof(EntityNotFoundException) },
        new object[]{"ManuViswan",typeof(Exception) },
    };
    #endregion
}
