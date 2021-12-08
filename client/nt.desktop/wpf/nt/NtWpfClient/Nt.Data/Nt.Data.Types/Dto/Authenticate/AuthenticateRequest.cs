using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Nt.Data.Types.Dto.Authenticate
{
    public class AuthenticateRequest
    {
        [JsonProperty("userName")]
        public string Username { get; set; }
        [JsonProperty("passKey")]
        public string Password { get; set; }
    }
}
