using Nt.Infrastructure.WebApi.ViewModels.Common;
using System;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.User.ValidateUser
{
    public class LoginResponse : IErrorInfo
    {
        public string UserName { get; set; }
        public bool IsAuthenticated { get; set; }
        public DateTime LoginTime { get; set; }
        public string AuthenticationToken { get; set; }
        public string DisplayName { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
        public string Token { get; set; }
        public string Bio { get; set; }
    }
}
