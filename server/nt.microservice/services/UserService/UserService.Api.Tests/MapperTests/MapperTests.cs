using UserService.Api.ViewModels.UserManagement;
using UserService.Service.Dtos;

namespace UserService.Api.Tests.MapperTests;

// Test only complex cases
public class MapperTests
{
    [Theory]
    [MemberData(nameof(UserProfileDto_To_SearchUserByUserNameResponseViewModel_TestData))]
    public void UserProfileDto_To_SearchUserByUserNameResponseViewModel_ShouldSucceed(UserProfileDto input, SearchUserByUserNameResponseViewModel expectedResult)
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<UserService.Api.Infrastructure.ProfileMap>());
        var mapper = configuration.CreateMapper();

        var response1 = mapper.Map<UserProfileViewModel>(input);
        var response = mapper.Map<SearchUserByUserNameResponseViewModel>(input);

        Assert.Equal(expectedResult, response);
    }

    public static IEnumerable<object[]> UserProfileDto_To_SearchUserByUserNameResponseViewModel_TestData => new List<object[]>  
    {
        new object[]
        {
            new UserProfileDto
            { 
                UserName = "Sample", 
                DisplayName = "Sample User", 
                Bio = "I am sample user", 
                FollowedBy = new List<long>(),
                Followers = new List<long>(),
            },
            new SearchUserByUserNameResponseViewModel
            {
                User = new UserProfileViewModel
                {
                    UserName = "Sample",
                    DisplayName = "Sample User",
                    Bio = "I am sample user",
                    FollowersCount = 0,
                    IsFollowing = false
                }
            }
        }
    };
}
