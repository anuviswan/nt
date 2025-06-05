namespace nt.orchestrator.AppHost.Settings;

public record InfrastructureSettings
{
    public string ApplicationRoot { get; set; } = null!;
    public Consul Consul { get; set; } = null!;
    public RabbitMq RabbitMq { get; set; } = null!;
    public Postgres Postgres { get; set; } = null!;
    public MongoDb MongoDb { get; set; } = null!;
    public BlobStorage BlobStorage { get; set; } = null!;
    public SqlServer SqlServer { get; set; } = null!;
}

public record Consul(string DockerImage,int HostPort, int TargetPort);
public record RabbitMq(string Host, string UserName,string Password,int HttpPort,int HttpsPort);
public record Postgres(string DockerImage,string UserName,string Password);
public record MongoDb(string UserName, string Password);
public record BlobStorage(string DockerImage, int HostPort, int TargetPort);
public record SqlServer(string Password,int HostPort, int TargetPort);


public record ServicesSettings
{
    public AuthService AuthService { get; set; } = null!;
}

public record AuthService
{
    public int[] InstancePorts { get; set; } = null!;
}


