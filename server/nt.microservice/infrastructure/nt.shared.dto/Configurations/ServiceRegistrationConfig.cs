namespace nt.shared.dto.Configurations;

public class ServiceRegistrationConfig
{
    public string RegistryUri { get; set; } = null!;
    public string ServiceName { get; set; } = null!;
    public string ServiceId { get; set; } = null!;
    public string ServiceHost { get; set; } = null!;
    public int ServicePort { get; set; }
    public string HealthCheckUrl { get; set; } = null!;
    public int DeregisterAfterMinutes { get; set; }
}
