namespace UserService.Api.Settings;

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
