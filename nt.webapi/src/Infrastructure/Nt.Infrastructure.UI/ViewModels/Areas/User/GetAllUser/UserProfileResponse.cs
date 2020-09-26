using Nt.Infrastructure.WebApi.ViewModels.Common;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.User.GetAllUser
{
    public class UserProfileResponse:IErrorInfo
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public int Rating { get; set; }
        public string Bio { get; set; } = string.Empty;
        public string ErrorMessage { get; set; }
    }
}
