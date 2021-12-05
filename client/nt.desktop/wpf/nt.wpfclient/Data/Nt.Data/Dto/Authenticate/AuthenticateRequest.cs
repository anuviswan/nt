using Newtonsoft.Json;

namespace Nt.Data.Dto.Authenticate
{
    public class AuthenticateRequest
    {
        [JsonProperty("userName")]
        public string Username { get; set; }
        [JsonProperty("passKey")]
        public string Password { get; set; }
    }
}
