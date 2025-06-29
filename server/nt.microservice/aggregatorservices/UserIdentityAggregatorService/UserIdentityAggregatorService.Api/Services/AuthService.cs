﻿
using Microsoft.Extensions.Options;
using nt.shared.dto.Configurations;
using UserIdentityAggregatorService.Api.Settings;

namespace UserIdentityAggregatorService.Api.Services;
public class AuthService : ServiceBase, IAuthService
{
    private readonly ILogger<AuthService> _logger;
    public AuthService(IHttpClientFactory httpClientFactory,
        ConsulServiceResolver consulResolver, ILogger<AuthService> logger,
        IOptions<ServiceMappingConfig> serviceMapping) : base(httpClientFactory, logger, consulResolver, serviceMapping, nameof(AuthService))
    {
        _logger = logger;
    }

    public async Task<AuthenticateResponseViewModel?> ValidateAsync(AuthorizeRequestViewModel request)
    {
        var response = new AuthenticateResponseViewModel();
        try
        {
            var client = await GetClientAsync();
            Console.WriteLine($"Validating user {client.BaseAddress}");
            var result = await client.PostAsJsonAsync("api/Authenticate/Validate", request).ConfigureAwait(false);
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