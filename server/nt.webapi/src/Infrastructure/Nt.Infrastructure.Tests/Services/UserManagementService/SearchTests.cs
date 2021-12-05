using Nt.Application.Services.User;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.RepositoryContracts.User;
using Nt.Infrastructure.Tests.Helpers;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;
using Nt.Infrastructure.Tests.Helpers.TestData;
using System.Linq.Expressions;

namespace Nt.Infrastructure.Tests.Services.UserManagementServiceTests;
public class SearchTests : ServiceTestBase<UserProfileEntity>
{
    public SearchTests(ITestOutputHelper output) : base(output)
    {

    }
    protected override void InitializeCollection()
    {
        Output.WriteLine("Initialized"); //TODO: Fix this removing this line causes InitializeCollection not being called randomly
        EntityCollection = MockDataHelper.UserCollection;
    }
    [Theory]
    [MemberData(nameof(SearchUserTestData))]
    [ServiceTest(nameof(UserManagementService)), Feature]
    public async Task SearchUser(string userName, IEnumerable<UserProfileEntity> expectedOutput)
    {
        var mockUserProfileRepository = new Mock<IUserProfileRepository>();
        mockUserProfileRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
                                    .Returns(Task.FromResult(EntityCollection.Where(x => (x.DisplayName.ToLower().StartsWith(userName.ToLower())
                                                                                            || x.UserName.ToLower().StartsWith(userName.ToLower()))
                                                                                            && x.IsDeleted == false)));

        var mockUnitOfwork = new Mock<IUnitOfWork>();
        mockUnitOfwork.SetupGet(x => x.UserProfileRepository).Returns(mockUserProfileRepository.Object);

        var userProfileService = new UserManagementService(mockUnitOfwork.Object);
        var result = await userProfileService.SearchUserAsync(userName);
        Assert.Equal(expectedOutput.Count() ,result.Count());
        foreach (var (expected, actual) in expectedOutput.OrderBy(x => x.UserName).Zip(result.OrderBy(c => c.UserName)))
        {
            Assert.Equal(expected.UserName, actual.UserName);
            Assert.Equal(expected.DisplayName, actual.DisplayName);
            Assert.Equal(expected.Followers.OrderBy(x => x), actual.Followers.OrderBy(x => x));
            Assert.Equal(expected.Bio, actual.Bio);
        }
    }

    public static IEnumerable<object[]> SearchUserTestData => new []
    {
        new object[]
        {
            $"{nameof(UserProfileEntity.UserName)} 1",
            new List<UserProfileEntity>
            {
                new ()
                {
                    Id = string.Format(Utils.MockUserIdFormat, 1),
                    UserName = $"{nameof(UserProfileEntity.UserName)} 1",
                    DisplayName = $"{nameof(UserProfileEntity.DisplayName)} 1",
                    Bio = $"{nameof(UserProfileEntity.Bio)} 1",
                    IsDeleted = false,
                    Followers = new[]
                    {
                        "UserName 3"
                    }
                },
                new ()
                {
                    Id = string.Format(Utils.MockUserIdFormat, 10),
                    UserName = $"{nameof(UserProfileEntity.UserName)} 10",
                    DisplayName = $"{nameof(UserProfileEntity.DisplayName)} 10",
                    Bio = $"{nameof(UserProfileEntity.Bio)} 10",
                    IsDeleted = false,
                    Followers = Enumerable.Empty<string>()
                }
            }
        },
        new object[]
        {
            $"{nameof(UserProfileEntity.UserName)} 1".ToLower(),
            new List<UserProfileEntity>
            {
                new ()
                {
                    Id = string.Format(Utils.MockUserIdFormat, 1),
                    UserName = $"{nameof(UserProfileEntity.UserName)} 1",
                    DisplayName = $"{nameof(UserProfileEntity.DisplayName)} 1",
                    Bio = $"{nameof(UserProfileEntity.Bio)} 1",
                    IsDeleted = false,
                    Followers = new[]
                    {
                        "UserName 3"
                    }
                },
                new ()
                {
                    Id = string.Format(Utils.MockUserIdFormat, 10),
                    UserName = $"{nameof(UserProfileEntity.UserName)} 10",
                    DisplayName = $"{nameof(UserProfileEntity.DisplayName)} 10",
                    Bio = $"{nameof(UserProfileEntity.Bio)} 10",
                    IsDeleted = false,
                    Followers = Enumerable.Empty<string>()
                }
            }
        },
        new object[]
        {
            $"{nameof(UserProfileEntity.UserName)} 1".ToUpper(),
            new List<UserProfileEntity>
            {
                new ()
                {
                    Id = string.Format(Utils.MockUserIdFormat, 1),
                    UserName = $"{nameof(UserProfileEntity.UserName)} 1",
                    DisplayName = $"{nameof(UserProfileEntity.DisplayName)} 1",
                    Bio = $"{nameof(UserProfileEntity.Bio)} 1",
                    IsDeleted = false,
                    Followers = new[]
                    {
                        "UserName 3"
                    }
                },
                new ()
                {
                    Id = string.Format(Utils.MockUserIdFormat, 10),
                    UserName = $"{nameof(UserProfileEntity.UserName)} 10",
                    DisplayName = $"{nameof(UserProfileEntity.DisplayName)} 10",
                    Bio = $"{nameof(UserProfileEntity.Bio)} 10",
                    IsDeleted = false,
                    Followers = Enumerable.Empty<string>()
                }
            }
        },
        new object[]
        {
            "Doesn'tExist", 
            Enumerable.Empty<UserProfileEntity>()
        },
        new object[]
        {
            string.Empty,
            Enumerable.Empty<UserProfileEntity>()
        },
        new object[]
        {
            " ",
            Enumerable.Empty<UserProfileEntity>()
        },
    };
}
