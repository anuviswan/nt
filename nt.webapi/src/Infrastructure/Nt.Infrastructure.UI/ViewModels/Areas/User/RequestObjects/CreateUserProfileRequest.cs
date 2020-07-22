namespace Nt.Infrastructure.WebApi.ViewModels.Areas.User.RequestObjects
{
    public class CreateUserProfileRequest
    {
        public string UserName { get; set; }
        public string PassKey { get; set; }
        public string DisplayName { get; set; }
        public string Bio { get; set; }
    }
}
