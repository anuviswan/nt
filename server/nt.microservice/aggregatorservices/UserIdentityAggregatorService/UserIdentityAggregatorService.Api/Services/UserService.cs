using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;
using UserIdentityAggregatorService.Api.Settings;

namespace UserIdentityAggregatorService.Api.Services;
public class UserService : ServiceBase, IUserService
{
    private readonly ILogger<UserService> _logger;
    public UserService(IHttpClientFactory httpClientFactory,
        ConsulServiceResolver consulResolver, ILogger<UserService> logger,
        IOptions<ServiceDiscoveryOptions> serviceDiscovery) : base(httpClientFactory, logger, consulResolver, serviceDiscovery, nameof(UserService))
    {
        _logger = logger;
    }
    public async Task<SearchUserByUserNameResponseViewModel?> SearchUserByUserNameAsync(string userName)
    {
        var client = await GetClientAsync();
        var response = await client.GetAsync($"api/UserManagement/SearchUserByUserName/{userName}");
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