using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Nt.Data.Types.Dto.BaseDto;

namespace Nt.Data.Types.Dto.Authenticate
{
    public class AuthenticateResponse : BaseResponse
    {
        [JsonProperty("UserName")]
        public string UserName { get; set; }
        [JsonProperty("DisplayName")]
        public string DisplayName { get; set; }
        [JsonProperty("Bio")]
        public string Bio { get; set; }
        [JsonProperty("Token")]
        public string AuthToken { get; set; }
    }
}
