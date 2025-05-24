public static class IResourceBuilderExtensions
{
    public static IResourceBuilder<ProjectResource> AddGateway(this IDistributedApplicationBuilder source)
    {
        return source.AddProject<Projects.nt_gateway>("nt-gateway-service");
    }
    public static IResourceBuilder<ProjectResource> AddUserIdentityAggregatorService(this IDistributedApplicationBuilder source)
    {
        return source.AddProject<Projects.UserIdentityAggregatorService_Api>("nt-useridentityaggregator-service");
    }

    public static IResourceBuilder<ProjectResource> AddAuthService(this IDistributedApplicationBuilder source)
    {
        var username = source.AddParameter("postgresUser", "postgres", secret: true);
        var password = source.AddParameter("postgressPassword", "Admin123", secret: true);

        var postgres = source.AddPostgres("UserSqlDb", username, password)
            .WithContainerName("nt.authservice.db")
            .WithImage("postgres:14.1-alpine")
            .WithPgAdmin()
            .WithDataVolume()
            .WithInitBindMount(@"D:\Source\nt\server\nt.microservice\services\Db\scripts");

        var server1 = source.AddProject<Projects.AuthService_Api>("nt-authservice-service1")
            .WithEnvironment("RUNNING_WITH", "aspire")
            .WithReference(postgres)
            .WaitFor(postgres)
            .WithHttpEndpoint(8101, name:"http1");

        var server2 = source.AddProject<Projects.AuthService_Api>("nt-authservice-service2")
            .WithEnvironment("RUNNING_WITH", "aspire")
            .WithReference(postgres)
            .WaitFor(postgres)
            .WithHttpEndpoint(8102, name:"http2");

        var server3 = source.AddProject<Projects.AuthService_Api>("nt-authservice-service3")
            .WithEnvironment("RUNNING_WITH", "aspire")
            .WithReference(postgres)
            .WaitFor(postgres)
            .WithHttpEndpoint(8103, name: "http3");

        var server4 = source.AddProject<Projects.AuthService_Api>("nt-authservice-service4")
            .WithEnvironment("RUNNING_WITH", "aspire")
            .WithReference(postgres)
            .WaitFor(postgres)
            .WithHttpEndpoint(8104, name: "http4");

        var server5 = source.AddProject<Projects.AuthService_Api>("nt-authservice-service5")
            .WithEnvironment("RUNNING_WITH", "aspire")
            .WithReference(postgres)
            .WaitFor(postgres)
            .WithHttpEndpoint(8105, name: "http5");

        // Run nginx
        var loadbalancer = source.AddContainer("nt-authservice-loadbalancer", "nginx:latest")
                                 .WithEndpoint(port: 8100, targetPort: 80)
                                 .WaitFor(server1);

        // Run Sidecar

        return server1;
    }

    public static IResourceBuilder<ProjectResource> AddMovieService(this IDistributedApplicationBuilder source)
    {
        return source.AddProject<Projects.MovieService_Api>("nt-movieservice-service");
    }

    public static IResourceBuilder<ProjectResource> AddUserService(this IDistributedApplicationBuilder source)
    {
        return source.AddProject<Projects.UserService_Api>("nt-userservice-service")
                .WithEnvironment("CONSUL_URL", "http://localhost:9500")
                .WithEnvironment("RUNNING_WITH", "aspire");
    }

    public static IResourceBuilder<ProjectResource> AddReviewService(this IDistributedApplicationBuilder source)
    {
        return source.AddProject<Projects.ReviewService_Api>("nt-reviewservice-service");
    }
}