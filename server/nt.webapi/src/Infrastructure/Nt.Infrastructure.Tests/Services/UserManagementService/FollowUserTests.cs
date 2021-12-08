using Nt.Application.Services.User;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.RepositoryContracts.User;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;
using Nt.Infrastructure.Tests.Helpers.TestData;
using System.Linq.Expressions;

namespace Nt.Infrastructure.Tests.Services.UserManagementServiceTests;
public class FollowUserTests : ServiceTestBase<UserProfileEntity>
{
    public FollowUserTests(ITestOutputHelper output) : base(output)
    {
    }
    protected override void InitializeCollection()
    {
        Output.WriteLine("Initialized"); //TODO: Fix this removing this line causes InitializeCollection not being called randomly
        EntityCollection = new (MockDataHelper.UserCollection);
    }


    #region Valid Cases 
    [Theory]
    [MemberData(nameof(FollowUserSuccessTestData))]
    [ServiceTest(nameof(UserManagementService)), Feature]
    public async Task FollowUserSuccess(string currentUserName,string userNameToFollow)
    {
        // Arrange
        var mockUserRepository = new Mock<IUserProfileRepository>();
        mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
                            .Returns((Expression<Func<UserProfileEntity, bool>> x) =>
                            Task.FromResult(EntityCollection.AsQueryable<UserProfileEntity>().Where(x).AsEnumerable()));

        mockUserRepository.Setup(x => x.UpdateAsync(It.IsAny<UserProfileEntity>()))
            .Callback((UserProfileEntity user) =>
            {
                var userToUpdate = EntityCollection.Single(x => string.Equals(x.UserName, user.UserName));
                userToUpdate = user.UserName switch
                {
                    var usrCurrent when usrCurrent == currentUserName => userToUpdate with { Follows = user.Follows },
                    var usrToFollow when usrToFollow == userNameToFollow => userToUpdate with { Followers = user.Followers },
                    _ => userToUpdate
                };
                    
                EntityCollection.Remove(EntityCollection.Single(x => string.Equals(x.UserName, user.UserName)));
                EntityCollection.Add(userToUpdate);
            });

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.UserProfileRepository).Returns(mockUserRepository.Object);

        // Act
        var userManagementService = new UserManagementService(mockUnitOfWork.Object);
        await userManagementService.FollowUserAsync(currentUserName,userNameToFollow);

        // Assert
        var followedUser = EntityCollection.Single(x => string.Equals(x.UserName, userNameToFollow));
        Assert.Contains(MockDataHelper.GetUser(currentUserName).Id, followedUser.Followers);

        var currentUser = EntityCollection.Single(x => string.Equals(x.UserName, currentUserName));
        Assert.Contains(MockDataHelper.GetUser(userNameToFollow).Id, currentUser.Follows);
    }

    public static IEnumerable<object[]> FollowUserSuccessTestData => new []
    {
        new object[]
        {
            "UserName 1",
            "UserName 2"
        },
        new object[]
        {
            "UserName 3",
            "UserName 4"
        }
    };
    #endregion


    #region Valid Cases 
    [Theory]
    [MemberData(nameof(FollowUserFailureTestData))]
    [ServiceTest(nameof(UserManagementService)), Feature]
    public async Task FollowUserFailure(string currentUser, string userToFollow)
    {
        // Arrange
        var mockUserRepository = new Mock<IUserProfileRepository>();
        mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
                            .Returns((Expression<Func<UserProfileEntity, bool>> x) =>
                            Task.FromResult(EntityCollection.AsQueryable<UserProfileEntity>().Where(x).AsEnumerable()));

        mockUserRepository.Setup(x => x.UpdateAsync(It.IsAny<UserProfileEntity>()))
            .Callback((UserProfileEntity user) =>
            {
                var userToUpdate = EntityCollection.Single(x => string.Equals(x.UserName, user.UserName));
                userToUpdate = userToUpdate with { Followers = user.Followers };
                EntityCollection.Remove(EntityCollection.Single(x => string.Equals(x.UserName, user.UserName)));
                EntityCollection.Add(userToUpdate);
            });

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.UserProfileRepository).Returns(mockUserRepository.Object);

        // Act
        var userManagementService = new UserManagementService(mockUnitOfWork.Object);
        await Assert.ThrowsAsync<EntityNotFoundException>(()=> userManagementService.FollowUserAsync(currentUser, userToFollow));

    }

    public static IEnumerable<object[]> FollowUserFailureTestData => new []
    {
        new object[]
        {
            "InvalidUser",
            "UserName 2",
        },
        new object[]
        {
            "UserName 3",
            "InvalidUser",
        },
        new object[]
        {
            string.Empty,
            "UserName 3",
        },

        new object[]
        {

            "UserName 3",
            string.Empty,
        },

        new object[]
        {
            null,
            "UserName 3",
        },

        new object[]
        {

            "UserName 3",
            null,
        },

    };
    #endregion
}
