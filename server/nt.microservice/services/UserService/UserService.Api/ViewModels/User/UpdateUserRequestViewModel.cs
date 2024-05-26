using System.Text.Json.Serialization;

namespace UserService.Api.ViewModels.User;

public record UpdateUserRequestViewModel
{
    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("bio")]
    public string? Bio { get; set; }
}
