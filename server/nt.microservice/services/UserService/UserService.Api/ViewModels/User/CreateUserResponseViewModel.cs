using System.Text.Json.Serialization;

namespace UserService.Api.ViewModels.User;
public record CreateUserResponseViewModel
{
    [JsonPropertyName("userName")]
    public string UserName { get; init; }

    [JsonPropertyName("displayName")]
    public string DisplayName { get;init; }
}
