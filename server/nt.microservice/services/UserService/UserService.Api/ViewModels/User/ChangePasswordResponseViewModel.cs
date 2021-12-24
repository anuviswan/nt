namespace UserService.Api.ViewModels.User;
public record ChangePasswordResponseViewModel
{
    public string UserName { get; init; }
    public bool IsPasswordChanged { get; init; }
}
