using Aspire.Hosting;
using Microsoft.AspNetCore.Components.Endpoints;

public static class IResourceBuilderExtensions
{

    public static IResourceBuilder<ProjectResource> AddAuthService(this IDistributedApplicationBuilder source, IResourceBuilder<ContainerResource> consul, IResourceBuilder<RabbitMQServerResource> rabbitMq)
    {
        var consulUrl = consul.GetEndpoint("http");
        var username = source.AddParameter("postgresUser", "postgres", secret: true);
        var password = source.AddParameter("postgressPassword", "Admin123", secret: true);

        var postgres = source.AddPostgres("UserSqlDb", username, password)
            .WithContainerName("nt.authservice.db")
            .WithImage("postgres:14.1-alpine")
            .WithPgAdmin()
            .WithDataVolume()
            .WithInitBindMount(@"D:\Source\nt\server\nt.microservice\services\Db\scripts")
            .AddDatabase("ntuserauth");

        var server1 = source.AddProject<Projects.AuthService_Api>("nt-authservice-service1")
            .WithEnvironment("RUNNING_WITH", "aspire")
            .WithReference(postgres)
            .WaitFor(postgres)
            .WithHttpEndpoint(8101, name:"http1")
            .WaitFor(consul);

        var server2 = source.AddProject<Projects.AuthService_Api>("nt-authservice-service2")
            .WithEnvironment("RUNNING_WITH", "aspire")
            .WithReference(postgres)
            .WaitFor(postgres)
            .WithHttpEndpoint(8102, name:"http2")
            .WaitFor(consul); 

        var server3 = source.AddProject<Projects.AuthService_Api>("nt-authservice-service3")
            .WithEnvironment("RUNNING_WITH", "aspire")
            .WithReference(postgres)
            .WaitFor(postgres)
            .WithHttpEndpoint(8103, name: "http3")
            .WaitFor(consul); 

        var server4 = source.AddProject<Projects.AuthService_Api>("nt-authservice-service4")
            .WithEnvironment("RUNNING_WITH", "aspire")
            .WithReference(postgres)
            .WaitFor(postgres)
            .WithHttpEndpoint(8104, name: "http4")
            .WaitFor(consul);

        var server5 = source.AddProject<Projects.AuthService_Api>("nt-authservice-service5")
            .WithEnvironment("RUNNING_WITH", "aspire")
            .WithReference(postgres)
            .WaitFor(postgres)
            .WithHttpEndpoint(8105, name: "http5")
            .WaitFor(consul);

        // Run nginx
        var loadbalancer = source.AddContainer("nt-authservice-loadbalancer", "nginx:latest")
                                 .WithEndpoint(port: 8100, targetPort: 80)
                                 .WithBindMount(@"D:\Source\nt\server\nt.microservice\services\AuthService\AuthService.LoadBalancer\nginx\nginx.conf", "/etc/nginx/nginx.conf", isReadOnly: true)
                                 .WaitFor(server1)
                                 .WaitFor(server2)
                                 .WaitFor(server3)
                                 .WaitFor(server4)
                                 .WaitFor(server5);

        // Run Sidecar

        source.AddProject<Projects.AuthService_LoadBalancer_ServiceDiscoverySideCar>("AuthService-LoadBalancer-Sidecar")
                                 .WithEnvironment("ConsulConfig__serviceName", "nt.authservice.loadbalancer")
                                 .WithEnvironment("ConsulConfig__serviceId", "authservice-1")
                                 .WithEnvironment("ConsulConfig__serviceAddress", $"host.docker.internal:8100")
                                 .WithEnvironment("ConsulConfig__servicePort", "80")
                                 .WithEnvironment("ConsulConfig__healthCheckUrl", "/health")
                                 .WithEnvironment("ConsulConfig__consulAddress", consulUrl)
                                 .WithEnvironment("ConsulConfig__deregisterAfterMinutes", "5")
                                 .WaitFor(consul)
                                 .WaitFor(rabbitMq)
                                 .WaitFor(loadbalancer);
        return server1;
    }

    public static IResourceBuilder<ProjectResource> AddMovieService(this IDistributedApplicationBuilder source)
    {
        var username = source.AddParameter("mongoDbUser", "root", secret: true);
        var password = source.AddParameter("mongoDbPassword", "mypass", secret: true);

        var mongoDb = source.AddMongoDB("nt-movieservice-db", 27017, userName:username, password:password )
            .WithEnvironment("MONGO_INITDB_ROOT_USERNAME", "root")
            .WithEnvironment("MONGO_INITDB_ROOT_PASSWORD", "mypass")
            //.WithEndpoint(port: 27017, targetPort: 27017, isProxied: true)
            .WithContainerName("nt.movieservice.db")
            .WithDataVolume()
            .WithMongoExpress();

        //var annotations =mongoDb.Resource.Annotations;

        //// Find the EndpointAnnotation for "tcp"
        //var endpointAnnotation = annotations
        //    .OfType<Aspire.Hosting.ApplicationModel.EndpointAnnotation>()
        //    .FirstOrDefault(a => a.Name == "tcp");

        //// Fetch the port (TargetPort or Port)
        //int? port = endpointAnnotation?.TargetPort ?? endpointAnnotation?.Port;

        // int port = mongoDb.Resource.PrimaryEndpoint.Port;

        return source.AddProject<Projects.MovieService_Api>("nt-movieservice-service")
            .WithEnvironment("MovieDatabase__DatabaseName", "ntmoviestore")
            .WithEnvironment("MovieDatabase__MovieCollectionName", "movies")
            .WithReference(mongoDb)
            .WaitFor(mongoDb);
            //.WithEnvironment("MovieDatabase__ConnectionString","mongodb://root:mypass@{nt-movieservice-db.bindings.tcp.host}:{nt-movieservice-db.bindings.tcp.port}/?authSource=admin");

        int port1 = mongoDb.Resource.PrimaryEndpoint.Port;
    }

    public static IResourceBuilder<ProjectResource> AddUserService(this IDistributedApplicationBuilder source, IResourceBuilder<RabbitMQServerResource> rabbitMq)
    {

        var blobStorage = source.AddContainer("nt-userservice-blobstorage", "mcr.microsoft.com/azure-storage/azurite")
        //    .WithVolume(@"./localstorage/data:/data")
            .WithArgs("azurite-blob", "--blobHost", "0.0.0.0", "-l", "/data")
            .WithHttpEndpoint(port:10000,targetPort:10000, isProxied: true);

        var passwordParameter = source.AddParameter("sqlServerPassword", "Admin123");
        var sqlDb = source.AddSqlServer("nt-userservice-db",passwordParameter)
                .WithEnvironment("ACCEPT_EULA", "Y")
                .WithEnvironment("MSSQL_SA_PASSWORD", "Admin123")
                .WithHttpEndpoint(port: 1433, targetPort: 1433, isProxied: true);

        return source.AddProject<Projects.UserService_Api>("nt-userservice-service")
                .WithEnvironment("CONSUL_URL", "http://localhost:9500")
                .WithEnvironment("RUNNING_WITH", "aspire")
                .WaitFor(blobStorage)
                .WaitFor(sqlDb);
    }

    public static IResourceBuilder<ProjectResource> AddReviewService(this IDistributedApplicationBuilder source)
    {
        return source.AddProject<Projects.ReviewService_Api>("nt-reviewservice-service");
    }
}