using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts.User;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;

namespace Nt.Infrastructure.Tests.Services.UserProfileServiceTests;
public class UserProfileServiceSearchTests : ServiceTestBase<UserProfileEntity>
{
    public UserProfileServiceSearchTests(ITestOutputHelper output):base(output)
    {

    }
    protected override void InitializeCollection()
    {
        EntityCollection = Enumerable.Range(1, 10).Select(x => new UserProfileEntity
        {
            DisplayName = $"User Name {x}",
            UserName = $"username{x}",
            PassKey = $"passKey{x}",
            Bio = $"bio{x}"
        }).ToList();
    }

    [Fact]
    [ServiceTest(nameof(UserProfileService)), Feature]
    public void GetUsers()
    {
        var mockUserProfileRepository = new Mock<IUserProfileRepository>();
    }

}
