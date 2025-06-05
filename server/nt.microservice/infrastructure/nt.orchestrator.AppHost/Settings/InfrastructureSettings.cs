namespace nt.orchestrator.AppHost.Settings;

public record InfrastructureSettings
{
    public string ApplicationRoot { get; set; } = null!;
    public Consul Consul { get; set; } = null!;
    public RabbitMq RabbitMq { get; set; } = null!;

}

public record Consul(string Container,int HttpPort, int TargetPort);
public record RabbitMq(string UserName,string Password,int HttpPort,int HttpsPort);

