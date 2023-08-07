namespace UserService.Api.ViewModels.UserManagement;

public record UserProfileViewModel(string UserName,string DisplayName, long Followers, bool IsFollowing);
