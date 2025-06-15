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

var serviceRegistrationConfig = configuration.GetSection(nameof(ServiceRegistrationConfig))
                                .Get<ServiceRegistrationConfig>();

Console.WriteLine("Attempting to register with Consul");

ArgumentNullException.ThrowIfNull(serviceRegistrationConfig, nameof(serviceRegistrationConfig));

var consulClient = new ConsulClient(x => x.Address = new Uri(serviceRegistrationConfig.RegistryUri));
var registration = new AgentServiceRegistration
{
    ID = serviceRegistrationConfig.ServiceId,
    Name = serviceRegistrationConfig.ServiceName,
    Address = serviceRegistrationConfig.ServiceHost,
    Port = serviceRegistrationConfig.ServicePort,
    Check = new AgentServiceCheck
    {
        HTTP = serviceRegistrationConfig.HealthCheckUrl,
        Interval = TimeSpan.FromSeconds(10),
        Timeout = TimeSpan.FromSeconds(5),
        DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(serviceRegistrationConfig.DeregisterAfterMinutes),
    }
};

// Register service with Consul
await consulClient.Agent.ServiceRegister(registration);
Console.WriteLine($"AuthService Load Balancer with Nginx registred successfully");
