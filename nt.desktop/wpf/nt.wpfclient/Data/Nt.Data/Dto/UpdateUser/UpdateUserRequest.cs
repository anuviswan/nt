using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.Data.Dto.UpdateUser
{
    public class UpdateUserRequest
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
        [JsonProperty("bio")]
        public string Bio { get; set; }
    }
}
