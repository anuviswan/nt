using FluentValidation;
using System.Text.Json.Serialization;

namespace AuthService.Api.ViewModels.Validate;
public class AuthorizeResponseViewModel
{
    [JsonPropertyName("token")]
    public string Token { get; set; }


    [JsonPropertyName("isAuthenticated")]
    public bool IsAuthenticated { get; set; }

    
    [JsonPropertyName("loginTime")]
    public DateTime LoginTime { get; set; }

    
    [JsonPropertyName("userName")]
    public string UserName { get; set; }

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; }

    [JsonPropertyName("bio")]
    public string Bio { get; set; }
}
