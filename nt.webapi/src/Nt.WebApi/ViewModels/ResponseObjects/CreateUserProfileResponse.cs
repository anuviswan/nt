using Nt.WebApi.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.WebApi.Models.ResponseObjects
{
    public class CreateUserProfileResponse:IErrorInfo
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
