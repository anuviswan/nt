using Nt.Infrastructure.WebApi.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.User.ChangePassword
{
    public class ChangePasswordResponse : IErrorInfo
    {
        public string ErrorMessage { get; set; }
    }
}
