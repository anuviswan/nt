namespace nt.shared.dto.Configurations;

public class ServiceDiscoveryConfiguration
{
    public string ServiceDiscoveryAddress { get; set; } = null!;
    public string ServiceName { get; set; } = null!;
    public string ServiceId { get; set; } = null!;
    public string ServiceAddress { get; set; } = null!;
    public int ServicePort { get; set; }
    public string HealthCheckUrl { get; set; } = null!;
    public int DeregisterAfterMinutes { get; set; }
}
