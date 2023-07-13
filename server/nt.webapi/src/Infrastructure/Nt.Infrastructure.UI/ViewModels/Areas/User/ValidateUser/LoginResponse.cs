namespace Nt.Infrastructure.WebApi.ViewModels.Areas.User.ValidateUser;
public record LoginResponse 
{
    public string UserName { get; init; }
    public bool IsAuthenticated { get; init; }
    public DateTime LoginTime { get; init; }
    public string DisplayName { get; init; }
    public string Token { get; init; }
    public string Bio { get; init; }
}
