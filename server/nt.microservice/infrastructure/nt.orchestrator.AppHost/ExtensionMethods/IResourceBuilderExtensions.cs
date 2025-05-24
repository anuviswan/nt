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
            .WithDataVolume();
            
        //var authDb = postgres.AddDatabase("ntuserauth");
        postgres.WithInitBindMount(@"D:\Source\nt\server\nt.microservice\services\Db\scripts"); ;

        return source.AddProject<Projects.AuthService_Api>("nt-authservice-service")
            .WithEnvironment("RUNNING_WITH", "aspire")
            .WithReference(postgres)
            .WaitFor(postgres);
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