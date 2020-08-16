using System.Text.Json.Serialization;

namespace Nt.Data.Dto.Authenticate
{
    public class AuthenticateRequest
    {
        [JsonPropertyName("userName")]
        public string Username { get; set; }
        [JsonPropertyName("passKey")]
        public string Password { get; set; }
    }
}
