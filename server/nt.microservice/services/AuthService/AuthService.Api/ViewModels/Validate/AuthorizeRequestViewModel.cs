using System.Text.Json.Serialization;

namespace AuthService.Api.ViewModels.Validate;
public record AuthorizeRequestViewModel
{
    [JsonPropertyName("userName")]
    public string UserName { get; set; } = null!;

    [JsonPropertyName("passKey")]
    public string Password { get; set; } = null!;  
}
