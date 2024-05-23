namespace AuthService.Api.ViewModels.AddUser;

public record AddUserResponseViewModel
{
    public required Guid Id { get; init; }
    public required string UserName { get; init; }
}
