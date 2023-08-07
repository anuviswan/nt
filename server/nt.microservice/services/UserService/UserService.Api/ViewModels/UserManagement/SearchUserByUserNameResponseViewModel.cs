namespace UserService.Api.ViewModels.UserManagement;

public record SearchUserByUserNameResponseViewModel
{
    public UserProfileViewModel User { get; set; }
}
