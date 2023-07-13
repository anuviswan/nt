using System.Text.Json.Serialization;

namespace AuthService.Api.ViewModels.Validate;
public class AuthorizeRequestViewModel
{
    [JsonPropertyName("userName")]
    public string UserName { get; set; }

    [JsonPropertyName("passKey")]
    public string Password { get; set; }    
}
