namespace AuthService.Api.ViewModels.AddUser;
public record AddUserRequestViewModel
{
    public string UserName { get; init; } = null!;
    public string Password { get; set; } = null!;
}
