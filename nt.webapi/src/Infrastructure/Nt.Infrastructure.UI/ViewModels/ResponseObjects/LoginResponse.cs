using Nt.Infrastructure.WebApi.ViewModels.Common;
using System;

namespace Nt.Infrastructure.WebApi.ViewModels.ResponseObjects
{
    public class LoginResponse : IErrorInfo
    {
        public string UserName { get; set; }
        public bool IsAuthenticated { get; set; }

        public DateTime LoginTime { get; set; }
        public string AuthenticationToken { get; set; }
        public string DisplayName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
