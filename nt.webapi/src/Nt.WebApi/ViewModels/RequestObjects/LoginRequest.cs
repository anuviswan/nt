using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.WebApi.ViewModels.RequestObjects
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string PassKey { get; set; }
    }
}
