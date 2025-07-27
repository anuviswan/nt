using Consul;
using Microsoft.Extensions.Options;
using nt.shared.dto.Configurations;

namespace ReviewService.Presenation.Api.BackgroundServices;

public class ConsulServiceRegistrationService : BackgroundService
{
    private readonly IConsulClient _consulClient;
    private readonly ServiceRegistrationConfig _serviceDiscoveryConfiguration;
    private readonly ILogger<ConsulServiceRegistrationService> _logger;
    private readonly IHostApplicationLifetime _lifetime;
    public ConsulServiceRegistrationService(IConsulClient consultClient,
        IOptions<ServiceRegistrationConfig> serviceDiscoveryConfigurations,
        ILogger<ConsulServiceRegistrationService> logger,
        IHostApplicationLifetime lifetime)
    {
        _consulClient = consultClient ?? throw new ArgumentNullException(nameof(consultClient));
        _serviceDiscoveryConfiguration = serviceDiscoveryConfigurations?.Value ?? throw new ArgumentNullException(nameof(serviceDiscoveryConfigurations));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _lifetime = lifetime ?? throw new ArgumentNullException(nameof(lifetime));
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var registration = new AgentServiceRegistration
        {
            ID = _serviceDiscoveryConfiguration.ServiceId,
            Name = _serviceDiscoveryConfiguration.ServiceName,
            Address = _serviceDiscoveryConfiguration.ServiceHost,
            Port = _serviceDiscoveryConfiguration.ServicePort,
            Check = new AgentServiceCheck
            {
                HTTP = _serviceDiscoveryConfiguration.HealthCheckUrl,
                Interval = TimeSpan.FromSeconds(10),
                Timeout = TimeSpan.FromSeconds(5),
                DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(3)
            }
        };

        // Register service with Consul
        await _consulClient.Agent.ServiceRegister(registration).ConfigureAwait(false);
        _logger.LogInformation($"User Service registered with Consul successfully with health check url {registration.Check.HTTP}");


        _lifetime.ApplicationStopping.Register(async () =>
        {
            _logger.LogInformation("Deregistering service from Consul");
            await _consulClient.Agent.ServiceDeregister(_serviceDiscoveryConfiguration.ServiceId);
        });
    }
}

