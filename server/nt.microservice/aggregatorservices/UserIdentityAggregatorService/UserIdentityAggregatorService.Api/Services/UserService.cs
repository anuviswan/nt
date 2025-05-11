using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using UserIdentityAggregatorService.Api.Services.AuthService;

namespace UserIdentityAggregatorService.Api.Services.UserService;

public class UserService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<UserService> _logger;
    public UserService(HttpClient httpClient, ILogger<UserService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    public async Task<SearchUserByUserNameResponseViewModel?> SearchUserByUserNameAsync(string userName)
    {
        var response = await _httpClient.GetAsync($"api/UserManagement/SearchUserByUserName/{userName}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SearchUserByUserNameResponseViewModel>(content);
        }
        else
        {
            _logger.LogError($"Error validating user : {response.StatusCode}");
        }
        return null;

    }
}

public record SearchUserByUserNameResponseViewModel
{
    [JsonPropertyName("user")]
    public UserProfileViewModel User { get; set; } = null!;
}

public record UserProfileViewModel
{
    [JsonPropertyName("userName")]
    public string UserName { get; init; } = null!;

    [JsonPropertyName("displayName")]
    public string? DisplayName { get; init; }

    [JsonPropertyName("bio")]
    public string? Bio { get; init; }

    [JsonPropertyName("followersCount")]
    public long FollowersCount { get; init; }

    [JsonPropertyName("isFollowing")]
    public bool IsFollowing { get; init; }
}