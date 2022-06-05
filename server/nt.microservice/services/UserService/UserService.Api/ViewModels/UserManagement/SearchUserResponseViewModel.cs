namespace UserService.Api.ViewModels.UserManagement;

public record SearchUserResponseViewModel
{
    public IEnumerable<UserMiniProfile> User { get; set; }
}

public record UserMiniProfile(string UserName,string DisplayName, long Followers, bool IsFollowing);
