using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nt.WebApi.Interfaces;

namespace Nt.WebApi.Models
{
    public class LoginDto:BaseEntity,IErrorInfo
    {
        public string DisplayName { get; set; }
        public DateTime LoggedInTime { get; set; }
        public string Tokenb { get; set; }
        public bool Validated { get; set; }
        public string ErrorMessage { get; set; }
    }
}
