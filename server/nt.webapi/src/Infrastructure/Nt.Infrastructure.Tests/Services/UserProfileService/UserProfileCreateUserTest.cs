using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.RepositoryContracts.User;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;
using System.Linq.Expressions;

namespace Nt.Infrastructure.Tests.Services.UserProfileServiceTests;
public class UserProfileCreateUserTest:ServiceTestBase<UserProfileEntity>
{
    public UserProfileCreateUserTest(ITestOutputHelper output):base(output)
    {

    }
    protected override void InitializeCollection()
    {
        EntityCollection = new()
        {
            new () { UserName = "AnuViswan", DisplayName = "Anu Viswan", IsDeleted = false },
            new () { UserName = "ManuViswan", DisplayName = "Manu Viswan", IsDeleted = false },
            new () { UserName = "AnuViswan", DisplayName = "AnuViswan", IsDeleted = true },
        };
    }

    [Theory]
    [MemberData(nameof(CreateUserTestSuccessTestCases))]
    [ServiceTest(nameof(UserProfileService)), Feature]
    public async Task CreateUserTestSuccess(UserProfileEntity request, UserProfileEntity response)
    {
        var mockUserProfileRepository = new Mock<IUserProfileRepository>();
        mockUserProfileRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
            .Returns(Task.FromResult(EntityCollection.Where(x => x.UserName.Equals(request.UserName, StringComparison.InvariantCultureIgnoreCase) && !x.IsDeleted)));
        mockUserProfileRepository.Setup(x => x.CreateAsync(request))
            .Returns((UserProfileEntity req)=>Task.FromResult(EntityCollection.Any(x=>x.UserName.Equals(req.UserName))?throw new Exception():request));
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.UserProfileRepository).Returns(mockUserProfileRepository.Object);
            
        var userProfileService = new Nt.Application.Services.User.UserProfileService(mockUnitOfWork.Object,null);
        var result = await userProfileService.CreateUserAsync(request);

        Assert.Equal(response.UserName, result.UserName);
        mockUserProfileRepository.Verify(mock => mock.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()), Times.Once());
        mockUserProfileRepository.Verify(mock => mock.CreateAsync(It.IsAny<UserProfileEntity>()), Times.Once());
    }

    public static IEnumerable<object[]> CreateUserTestSuccessTestCases => new []
    {
        new object[]
        {
            new UserProfileEntity
            {
                UserName="anuv",
                DisplayName = "Display Name"
            }, 
            new UserProfileEntity
            {
                UserName="anuv",
                DisplayName = "Display Name"
            } 
        }
    };

    [Theory]
    [MemberData(nameof(CreateUserTestExceptionTestCases))]
    [ServiceTest(nameof(UserProfileService)), Feature]
    public async Task CreateUserTestException(UserProfileEntity request, UserProfileEntity response)
    {
        var mockUserProfileRepository = new Mock<IUserProfileRepository>();

        mockUserProfileRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
            .Returns(Task.FromResult(EntityCollection.Where(x => x.UserName.Equals(request.UserName, StringComparison.InvariantCultureIgnoreCase) && !x.IsDeleted)));
        mockUserProfileRepository.Setup(x => x.CreateAsync(request))
            .Returns((UserProfileEntity req) => Task.FromResult(request));

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.UserProfileRepository).Returns(mockUserProfileRepository.Object);

        var userProfileService = new Nt.Application.Services.User.UserProfileService(mockUnitOfWork.Object,null);
        await Assert.ThrowsAsync<UserNameExistsException>(()=> userProfileService.CreateUserAsync(request));
        mockUserProfileRepository.Verify(mock => mock.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()), Times.Once());
        mockUserProfileRepository.Verify(mock => mock.CreateAsync(It.IsAny<UserProfileEntity>()), Times.Never());

    }

    public static IEnumerable<object[]> CreateUserTestExceptionTestCases => new []
    {
        new object[]
        {
            new UserProfileEntity
            {
                UserName="anuviswan",
                DisplayName = "Display Name"
            }, 
            new UserProfileEntity

            {UserName="anuviswan",
                DisplayName = "Display Name"
            } 
        }
    };
}
