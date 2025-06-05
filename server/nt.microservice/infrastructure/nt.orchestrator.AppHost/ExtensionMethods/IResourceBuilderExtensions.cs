using Aspire.Hosting;
using Microsoft.AspNetCore.Components.Endpoints;
using static nt.helper.Constants.Infrastructure;

public static class IResourceBuilderExtensions
{

    

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
                .WithEnvironment("RUNNING_WITH", "aspire")
                .WithEnvironment("RabbitMqSettings__host", "localhost")
            .WithEnvironment("RabbitMqSettings__username", "ntuser")
            .WithEnvironment("RabbitMqSettings__password", "pass")
                .WithHttpEndpoint(8301,name:"http")
                .WaitFor(blobStorage)
                .WithReference(sqlDb)
                .WaitFor(sqlDb);
    }

    public static IResourceBuilder<ProjectResource> AddReviewService(this IDistributedApplicationBuilder source)
    {
        return source.AddProject<Projects.ReviewService_Api>("nt-reviewservice-service")
            .WithUrls(c => c.Urls.ForEach(u => u.DisplayText = $"Open API ({u.Endpoint?.EndpointName})")); 
    }
}