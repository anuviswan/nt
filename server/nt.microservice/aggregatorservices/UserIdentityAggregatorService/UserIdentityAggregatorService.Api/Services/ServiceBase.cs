using Microsoft.Extensions.Options;
using nt.shared.dto.Configurations;

namespace UserIdentityAggregatorService.Api.Services;

public abstract class ServiceBase
{
    protected readonly Task<HttpClient> _httpClientTask;
    protected readonly ConsulServiceResolver _consulResolver;
    protected ServiceBase(IHttpClientFactory httpClientFactory, ILogger<ServiceBase> logger, ConsulServiceResolver consulResolver, 
        IOptions<ServiceMappingConfig> serviceMappingConfig, string serviceName)
    {
        _consulResolver = consulResolver;
        var registeredService = serviceMappingConfig.Value.Services.FirstOrDefault(s => s.Key == serviceName)?.Name;
        if (registeredService == null)
        {
            logger.LogError("Service {ServiceName} not found in service discovery options", serviceName);
            throw new ArgumentException($"Service {serviceName} not found in service discovery options");
        }

        _httpClientTask = InitializeHttpClientAsync(httpClientFactory, registeredService);
    }

    private async Task<HttpClient> InitializeHttpClientAsync(IHttpClientFactory httpClientFactory, string serviceName)
    {
        var client = httpClientFactory.CreateClient(); // unnamed/default
        var (address, port) = await _consulResolver.ResolveServiceAsync(serviceName);
        client.BaseAddress = new Uri($"http://{address}:{port}");
        return client;
    }

    protected Task<HttpClient> GetClientAsync()
    {
        return _httpClientTask;
    }
}
