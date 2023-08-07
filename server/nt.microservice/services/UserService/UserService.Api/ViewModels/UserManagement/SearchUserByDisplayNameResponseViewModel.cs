namespace UserService.Api.ViewModels.UserManagement;

public record SearchUserByDisplayNameResponseViewModel
{
    public IEnumerable<UserProfileViewModel> User { get; set; }
}
