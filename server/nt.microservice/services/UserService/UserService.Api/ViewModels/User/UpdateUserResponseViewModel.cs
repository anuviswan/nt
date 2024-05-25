namespace UserService.Api.ViewModels.User;

public record UpdateUserResponseViewModel
{
    public string? DisplayName { get; set; }
    public string? Bio {  get; set; }
}
