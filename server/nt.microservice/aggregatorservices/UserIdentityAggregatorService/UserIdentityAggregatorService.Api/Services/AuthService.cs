using System.Text.Json.Serialization;

namespace UserIdentityAggregatorService.Api.Services.AuthService;
public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AuthService> _logger;
    public AuthService(HttpClient httpClient, ILogger<AuthService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<AuthenticateResponseViewModel?> ValidateAsync(AuthorizeRequestViewModel request)
    {
        var response = new AuthenticateResponseViewModel();
        try
        {
            var result = await _httpClient.PostAsJsonAsync("api/Authenticate/Validate", request).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                response = await result.Content.ReadFromJsonAsync<AuthenticateResponseViewModel>().ConfigureAwait(false);
            }
            else
            {
                _logger.LogError($"Error validating user : {result.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
        return response;
    }
}

public record AuthorizeRequestViewModel
{
    public string UserName { get; set; } = null!;

    public string PassKey { get; set; } = null!;
}

public class AuthenticateResponseViewModel
{
    public string? Token { get; set; }


    public bool IsAuthenticated { get; set; }


    public DateTime LoginTime { get; set; }


    public string UserName { get; set; } = null!;

    public string? DisplayName { get; set; }

    public string? Bio { get; set; }
}