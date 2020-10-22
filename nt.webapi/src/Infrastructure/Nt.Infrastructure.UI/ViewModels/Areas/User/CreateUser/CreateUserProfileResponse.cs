namespace Nt.Infrastructure.WebApi.ViewModels.Areas.User.CreateUser
{
    public record CreateUserProfileResponse
    {
        public string UserName { get; init; }
        public string DisplayName { get; init; }
    }
}
