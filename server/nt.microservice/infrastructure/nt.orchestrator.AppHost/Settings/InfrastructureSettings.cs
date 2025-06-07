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

public record LoadBalancer(string DockerImage, int HostPort, int TargetPort);


public record ServicesSettings
{
    public AuthService AuthService { get; set; } = null!;
    public Gateway Gateway { get; set; } = null!;

    public AggregateAuthUserService AggregateAuthUserService { get; set; } = null!;

    public UserService UserService { get; set; } = null!;

    public MovieService MovieService { get; set; } = null!;
}

public record Gateway(string Host);

public record AggregateAuthUserService
{
    public ServiceDiscovery ServiceDiscoveryOptions { get; set; } = null!;
    public record ServiceDiscovery
    {
        public List<Service> Services { get; set; } = [];
        public string ResolverName { get; set; } = null!;
        public string ResolverPort { get; set; } = null!;
    }

    public record Service(string Key, string Name);
}

public record AuthService
{
    public int[] InstancePorts { get; set; } = null!;

    public LoadBalancer LoadBalancer { get; set; } = null!;
    
    public SideCar ConsulSideCar { get; set; } = null!;
    public record SideCar
    {
        public string ServiceName { get; set; } = null!;
        public string ServiceId { get; set; } = null!;
        public string ServiceAddress { get; set; } = null!;
        public int ServicePort { get; set; }
        public string HealthCheckUrl { get; set; } = null!;
        public int DeregisterAfterMinutes { get; set; } = 5; // Default to 5 minutes
    }    
}

public record UserService
{
    public ServiceDiscoveryConfig ServiceDiscovery{ get; set; } = null!;
    public record ServiceDiscoveryConfig
    {
        public string ServiceName { get; set; } = null!;
        public string ServiceId { get; set; } = null!;
        public string ServiceAddress { get; set; } = null!;
        public int ServicePort { get; set; }
        public string HealthCheckUrl { get; set; } = null!;
        public int DeregisterAfterMinutes { get; set; } = 5; // Default to 5 minutes
    }
}

public record MovieService(string DbName,string MovieCollectionName);






