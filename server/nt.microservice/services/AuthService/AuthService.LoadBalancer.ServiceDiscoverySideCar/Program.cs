/*
 *  This application acts as a sidecar for registering Nginx Load Balancer 
 *  with Consul Service Discovery
 */

using Consul;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Starting Load Balancer Side car for registering with Consul");
Console.WriteLine("Reading Configuration");
var configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .Build();

var consulConfig = configuration.GetSection(nameof(ConsulConfig))
                                .Get<ConsulConfig>();

Console.WriteLine("Attempting to register with Consul");

ArgumentNullException.ThrowIfNull(consulConfig,nameof(consulConfig));

var consulClient = new ConsulClient(x=>x.Address = new Uri(consulConfig.ConsulAddress));
var registration = new AgentServiceRegistration
{
    ID = consulConfig.ServiceId,
    Name = consulConfig.ServiceName,
    Address = consulConfig.ServiceAddress,
    Port = consulConfig.ServicePort,
    Check = new AgentServiceCheck
    {
        HTTP = $"http://{consulConfig.ServiceAddress}{consulConfig.HealthCheckUrl}",
        Interval = TimeSpan.FromSeconds(10),
        Timeout = TimeSpan.FromSeconds(5),
        DeregisterCriticalServiceAfter = TimeSpan.FromMicroseconds(consulConfig.DeregisterAfterMinutes),
    }
};



// Register service with Consul
await consulClient.Agent.ServiceRegister(registration);
Console.WriteLine($"AuthService Load Balancer with Nginx registred successfully");


public record ConsulConfig
{
    public string ConsulAddress { get; set; } = null!;
    public string ServiceName { get; set; } = null!;
    public string ServiceId { get; set; } = null!;
    public string ServiceAddress { get; set; } = null!;
    public int ServicePort { get; set; } 
    public string HealthCheckUrl { get; set; } = null!;
    public int DeregisterAfterMinutes { get; set; }
}
