using System;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.User.ValidateUser
{
    public class LoginResponse : ResponseBase
    {
        public string UserName { get; set; }
        public bool IsAuthenticated { get; set; }
        public DateTime LoginTime { get; set; }
        public string AuthenticationToken { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string Bio { get; set; }
    }
}
