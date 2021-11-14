namespace Nt.Infrastructure.WebApi.ViewModels.Areas.User.GetAllUser;
public record UserProfileResponse
{
    public string UserName { get; init; }
    public string DisplayName { get; init; }
    public int Rating { get; init; }
    public string Bio { get; init; } = string.Empty;
    public IEnumerable<string> Followers { get; init; }
}
