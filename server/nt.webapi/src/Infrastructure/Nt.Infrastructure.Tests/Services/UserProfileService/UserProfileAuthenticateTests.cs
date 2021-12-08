using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.RepositoryContracts.User;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;
using System.Linq.Expressions;
using static System.Convert;

namespace Nt.Infrastructure.Tests.Services.UserProfileServiceTests;
public class UserProfileAuthenticateTests : ServiceTestBase<UserProfileEntity>
{
    public UserProfileAuthenticateTests(ITestOutputHelper output) : base(output)
    {

    }

    protected override void InitializeCollection()
    {
        Output.WriteLine("Initialized"); //TODO: Fix this removing this line causes InitializeCollection not being called randomly

        EntityCollection = new()
        {
            new () 
            { 
                UserName = "AnuViswan", 
                DisplayName = "Anu Viswan", 
                PassKey = ToBase64String(ASCIIEncoding.ASCII.GetBytes("passkeyanuviswan")), 
                IsDeleted = false,
                Bio="UserBio" 
            },
            new ()
            { 
                UserName = "ManuViswan", 
                DisplayName = "Manu Viswan", 
                PassKey = ToBase64String(ASCIIEncoding.ASCII.GetBytes("passkeyManuviswan")), 
                IsDeleted = false, 
                Bio = "UserBio" 
            },
            new () 
            { 
                UserName = "AnuViswan", 
                DisplayName = "AnuViswan", 
                PassKey = ToBase64String(ASCIIEncoding.ASCII.GetBytes("userDeleted")), 
                IsDeleted = true, 
                Bio = "UserBio" 
            },
        };
    }

    [Theory]
    [MemberData(nameof(AuthenticationFailureTestData))]
    [ServiceTest(nameof(UserProfileService)), Feature]
    public async Task AuthenticationFailureTest(UserProfileEntity userProfile)
    {
        var mockUserProfileRepository = new Mock<IUserProfileRepository>();
        mockUserProfileRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
            .Returns(Task.FromResult(EntityCollection.Where(x => userProfile.UserName.ToLower() == x.UserName.ToLower() && userProfile.PassKey == x.PassKey && !x.IsDeleted)));

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.UserProfileRepository).Returns(mockUserProfileRepository.Object);

        var userProfileService = new Nt.Application.Services.User.UserProfileService(mockUnitOfWork.Object,null);
        await Assert.ThrowsAsync<InvalidPasswordOrUsernameException>(() => userProfileService.AuthenticateAsync(userProfile));
    }

    public static IEnumerable<object[]> AuthenticationFailureTestData => new []
    {
        new object[]
        {
            new UserProfileEntity
            {
                UserName="anuviswan", 
                PassKey = ToBase64String(ASCIIEncoding.ASCII.GetBytes("passKeyanuviswan")), 
                IsDeleted = false 
            } 
        },
        new object[]
        {
            new UserProfileEntity
            {
                UserName="Mnuviswan", 
                PassKey = ToBase64String(ASCIIEncoding.ASCII.GetBytes("passkeyManuviswan")), 
                IsDeleted = false 
            } 
        },
        new object[]
        {
            new UserProfileEntity
            {
                UserName="anuviswan", 
                PassKey = ToBase64String(ASCIIEncoding.ASCII.GetBytes("userDeleted")), 
                IsDeleted = true 
            } 
        }
    };


    [Theory]
    [MemberData(nameof(AuthenticationSuccessTestData))]
    [ServiceTest(nameof(UserProfileService)), Feature]
    public async Task AuthenticationSuccessTest(UserProfileEntity userProfile)
    {
        var mockUserProfileRepository = new Mock<IUserProfileRepository>();
        mockUserProfileRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
            .Returns(Task.FromResult(EntityCollection.Where(x => userProfile.UserName.ToLower() == x.UserName.ToLower() && userProfile.PassKey == x.PassKey && !x.IsDeleted)));

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.UserProfileRepository).Returns(mockUserProfileRepository.Object);

        var userProfileService = new Nt.Application.Services.User.UserProfileService(mockUnitOfWork.Object,null);
        var result = await userProfileService.AuthenticateAsync(userProfile);

        Assert.Equal(userProfile.UserName.ToLower(), result.UserName.ToLower());
    }

    public static IEnumerable<object[]> AuthenticationSuccessTestData => new []
    {
        new object[]
        {
            new UserProfileEntity
            {
                UserName="anuviswan", 
                PassKey = ToBase64String(ASCIIEncoding.ASCII.GetBytes("passkeyanuviswan")), 
                IsDeleted = false, 
                Bio = "UserBio" 
            } 
        },
        new object[]
        {
            new UserProfileEntity
            {
                UserName="Manuviswan", 
                PassKey = ToBase64String(ASCIIEncoding.ASCII.GetBytes("passkeyManuviswan")), 
                IsDeleted = false, 
                Bio = "UserBio" 
            } 
        },
    };

}
