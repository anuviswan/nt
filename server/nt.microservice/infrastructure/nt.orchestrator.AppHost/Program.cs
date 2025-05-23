using Projects;
using Aspire.Hosting.Postgres;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.nt_gateway>("nt-gateway-service");

builder.AddProject<Projects.UserIdentityAggregatorService_Api>("nt-useridentityaggregator-service");

var consul = builder.AddContainer("consul", "hashicorp/consul:latest")
    .WithContainerName("nt.common.servicediscovery")
    .WithEndpoint(port:8500, targetPort:9500)
    .WithArgs("agent", "-dev", "-client=0.0.0.0"); // dev mode

var authDb = builder.AddPostgres("postgres")
    .WithContainerName("nt.authservice.db")
    .WithImage("postgres:14.1-alpine")
    .WithEnvironment("POSTGRES_USER", "postgres")
    .WithEnvironment("POSTGRES_PASSWORD", "Admin123")
    .WithEnvironment("POSTGRES_DB", "ntuserauth")
    .WithVolume("nt.authservice.db.volume", "/var/lib/postgresql/data");

builder.AddProject<Projects.AuthService_Api>("nt-authservice-service")
        .WithReference(authDb)
        .WaitFor(authDb);

builder.AddProject<Projects.MovieService_Api>("nt-movieservice-service");

builder.AddProject<Projects.UserService_Api>("nt-userservice-service")
        .WithEnvironment("CONSUL_URL", "http://localhost:9500")
        .WithEnvironment("RUNNING_WITH", "aspire");

builder.AddProject<Projects.ReviewService_Api>("nt-reviewservice-service");


builder.Build().Run();


public record ProjectReference
{
    public string Key { get; set; }
    public string? DockerFile { get; set; } 
}


public static class ProjectCollection
{
    public static ProjectReference Gateway { get; set; } = new ProjectReference 
    { 
        Key = "nt-gateway", 
        DockerFile = GetDockerFile(new Projects.nt_gateway().ProjectPath)
    };


    private static string GetDockerFile(string projectPath)
    {
        return Path.Combine(Path.GetDirectoryName(projectPath)!, "Dockerfile");
    }
}