using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace UserIdentityAggregatorService.Api.Services.UserService;

public class UserService
{
    private readonly HttpClient _httpClient;
    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<SearchUserByUserNameResponseViewModel?> SearchUserByUserNameAsync(SearchUserByUserNameRequestViewModel request)
    {
        var response = await _httpClient.GetAsync($"api/UserManagement/SearchUserByUserName?userName={request.UserName}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SearchUserByUserNameResponseViewModel>(content);
    }
}

public record SearchUserByUserNameRequestViewModel
{
    [Required(ErrorMessage = $"{nameof(UserName)} is mandatory.")]
    public string UserName { get; set; } = null!;
}
public record SearchUserByUserNameResponseViewModel
{
    public UserProfileViewModel User { get; set; } = null!;
}

public record UserProfileViewModel
{
    public string UserName { get; init; } = null!;
    public string? DisplayName { get; init; }
    public string? Bio { get; init; }
    public long FollowersCount { get; init; }
    public bool IsFollowing { get; init; }
}