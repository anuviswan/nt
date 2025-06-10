/*
 *  This application acts as a sidecar for registering Nginx Load Balancer 
 *  with Consul Service Discovery
 */

using Consul;
using Microsoft.Extensions.Configuration;
using nt.shared.dto.Configurations; // Add this using for ServiceDiscoveryConfiguration

Console.WriteLine("Starting Load Balancer Side car for registering with Consul");
Console.WriteLine("Reading Configuration");
var configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .Build();

var serviceDiscoveryConfig = configuration.GetSection(nameof(ServiceDiscoveryConfiguration))
                                .Get<ServiceDiscoveryConfiguration>();

Console.WriteLine("Attempting to register with Consul");

ArgumentNullException.ThrowIfNull(serviceDiscoveryConfig, nameof(serviceDiscoveryConfig));

var consulClient = new ConsulClient(x => x.Address = new Uri(serviceDiscoveryConfig.ServiceDiscoveryAddress));
var registration = new AgentServiceRegistration
{
    ID = serviceDiscoveryConfig.ServiceId,
    Name = serviceDiscoveryConfig.ServiceName,
    Address = serviceDiscoveryConfig.ServiceAddress,
    Port = serviceDiscoveryConfig.ServicePort,
    Check = new AgentServiceCheck
    {
        HTTP = serviceDiscoveryConfig.HealthCheckUrl,
        Interval = TimeSpan.FromSeconds(10),
        Timeout = TimeSpan.FromSeconds(5),
        DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(serviceDiscoveryConfig.DeregisterAfterMinutes),
    }
};

// Register service with Consul
await consulClient.Agent.ServiceRegister(registration);
Console.WriteLine($"AuthService Load Balancer with Nginx registred successfully");
