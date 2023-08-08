namespace UserService.Api.ViewModels.UserManagement;

public record UserProfileViewModel
{
    public string UserName { get; init; }
    public string DisplayName { get; init; }
    public string Bio { get; init; }
    public long FollowersCount { get; init; }
    public bool IsFollowing { get; init; }
}
