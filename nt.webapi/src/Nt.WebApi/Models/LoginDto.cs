using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.WebApi.Models
{
    public class LoginDto:BaseDto
    {
        public string DisplayName { get; set; }
        public DateTime LoggedInTime { get; set; }
        public string Tokenb { get; set; }
        public bool Validated { get; set; }
    }
}
