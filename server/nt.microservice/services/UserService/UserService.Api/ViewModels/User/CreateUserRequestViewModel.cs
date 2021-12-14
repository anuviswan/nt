namespace UserService.Api.ViewModels.User;
public record CreateUserRequestViewModel
{
    public string UserName { get; init; }
    public string Password { get; init; }
    public string DisplayName { get; init; }
    public string Bio { get; init; }
}
