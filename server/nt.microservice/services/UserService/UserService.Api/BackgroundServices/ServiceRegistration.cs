﻿
using Consul;
using Microsoft.Extensions.Options;
using nt.shared.dto.Configurations;

namespace UserService.Api.BackgroundServices;

public class ServiceRegistration : BackgroundService
{
    private readonly IConsulClient _consulClient;
    private readonly ServiceRegistrationConfig _serviceDiscoveryConfiguration;
    private readonly ILogger<ServiceRegistration> _logger;
    private readonly IHostApplicationLifetime _lifetime;
    public ServiceRegistration(IConsulClient consultClient,
        IOptions<ServiceRegistrationConfig> serviceDiscoveryConfigurations,
        ILogger<ServiceRegistration> logger,
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
                DeregisterCriticalServiceAfter = TimeSpan.FromMicroseconds(_serviceDiscoveryConfiguration.DeregisterAfterMinutes),
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
