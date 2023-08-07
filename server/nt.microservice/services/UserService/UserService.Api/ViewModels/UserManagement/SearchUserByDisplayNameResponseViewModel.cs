namespace UserService.Api.ViewModels.UserManagement;

public record SearchUserByDisplayNameResponseViewModel
{
    public IEnumerable<UserProfileViewModel> Users { get; set; }
}
