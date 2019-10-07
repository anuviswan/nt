using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.WebApi.Models.RequestObjects
{
    public class CreateUserProfileRequest
    {
        public string UserName { get; set; }
        public string PassKey { get; set; }
        public string DisplayName { get; set; }
    }
}
