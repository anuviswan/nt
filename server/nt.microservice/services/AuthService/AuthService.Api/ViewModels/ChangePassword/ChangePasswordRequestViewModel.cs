namespace AuthService.Api.ViewModels.ChangePassword;

public class ChangePasswordRequestViewModel
{
    public string OldPassword { get;set; } = null!;
    public string NewPassword { get; set; } = null!;
}
