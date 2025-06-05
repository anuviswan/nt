using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using nt.orchestrator.AppHost.Settings;

var hostBuilder = Host.CreateApplicationBuilder(args);
var config = hostBuilder.Configuration;
var infrastructureSettings = config.GetSection(nameof(InfrastructureSettings)).Get<InfrastructureSettings>();

ArgumentNullException.ThrowIfNull(infrastructureSettings, nameof(infrastructureSettings));
var root = infrastructureSettings.ApplicationRoot;

var builder = DistributedApplication.CreateBuilder(args);

var consulServiceDiscovery = builder.AddContainer(Constants.Infrastructure.Consul.ServiceName, infrastructureSettings.Consul.DockerImage)
            .WithContainerName(Constants.Infrastructure.Consul.ContainerName)
            .WithHttpEndpoint(port: infrastructureSettings.Consul.HostPort, targetPort: infrastructureSettings.Consul.TargetPort)
            .WithArgs("agent", "-dev", "-client=0.0.0.0"); // dev mode


var rabbitMqusername = builder.AddParameter(Constants.Infrastructure.RabbitMq.UserNameKey, infrastructureSettings.RabbitMq.UserName, secret: true);
var rabbitMqpassword = builder.AddParameter(Constants.Infrastructure.RabbitMq.PasswordKey, infrastructureSettings.RabbitMq.Password, secret: true);
var rabbitmq = builder.AddRabbitMQ(Constants.Infrastructure.RabbitMq.ServiceName, rabbitMqusername, rabbitMqpassword)
            .WithContainerName(Constants.Infrastructure.RabbitMq.ContainerName)
            .WithBindMount(source: @$"{root}/services/transportservices/rabbitmq-enabled-plugins",
                           target: @"/etc/rabbitmq/enabled_plugins")
            .WithBindMount(source: @$"{root}/services/transportservices/rabbitmq.config",
                           target: @"/etc/rabbitmq/rabbitmq.conf")
            .WithBindMount(source: @$"{root}/services/transportservices/rabbitmq-defs.json",
                           target: @"/etc/rabbitmq/definitions.json")
            .WithHttpEndpoint(infrastructureSettings.RabbitMq.HttpPort,targetPort: infrastructureSettings.RabbitMq.HttpPort, name:"http1")
            .WithHttpEndpoint(infrastructureSettings.RabbitMq.HttpsPort, targetPort: infrastructureSettings.RabbitMq.HttpsPort, name: "http2")
            .WithUrls(c => c.Urls.ForEach(u => u.DisplayText = $"Manage ({u.Endpoint?.EndpointName})")); 


var postgresUsername = builder.AddParameter(Constants.AuthService.Database.UserNameKey, infrastructureSettings.Postgres.UserName, secret: true);
var postgresPassword = builder.AddParameter(Constants.AuthService.Database.PasswordKey, infrastructureSettings.Postgres.Password, secret: true);

var postgres = builder.AddPostgres(Constants.AuthService.Database.InstanceName, postgresUsername, postgresPassword)
            .WithContainerName(Constants.AuthService.Database.ContainerName)
            .WithImage(infrastructureSettings.Postgres.DockerImage)
            .WithPgAdmin()
            .WithDataVolume()
            .WithInitBindMount($@"{root}\services\Db\scripts\Aspire");

var mongoDbUsername = builder.AddParameter(Constants.MovieService.Database.UserNameKey, infrastructureSettings.MongoDb.UserName, secret: true);
var mongoDbPassword = builder.AddParameter(Constants.MovieService.Database.PasswordKey, infrastructureSettings.MongoDb.Password, secret: true);

var mongoDb = builder.AddMongoDB(Constants.MovieService.Database.InstanceName, 27017, userName: mongoDbUsername, password: mongoDbPassword)
            .WithEnvironment(Constants.MovieService.EnvironmentVariable.DbUserNameKey, infrastructureSettings.MongoDb.UserName)
            .WithEnvironment(Constants.MovieService.EnvironmentVariable.DbPasswordKey, infrastructureSettings.MongoDb.Password)
            //.WithEndpoint(port: 27017, targetPort: 27017, isProxied: true)
            .WithContainerName(Constants.MovieService.Database.ContainerName)
            .WithDataVolume()
            .WithMongoExpress();

var blobStorage = builder.AddContainer("nt-userservice-blobstorage", infrastructureSettings.BlobStorage.DockerImage)
            //    .WithVolume(@"./localstorage/data:/data")
            .WithArgs("azurite-blob", "--blobHost", "0.0.0.0", "-l", "/data")
            .WithHttpEndpoint(port: infrastructureSettings.BlobStorage.HostPort, targetPort: infrastructureSettings.BlobStorage.TargetPort, isProxied: true);






var sqlServerPassword = builder.AddParameter("sqlServerPassword", "Admin123");
var sqlDb = builder.AddSqlServer("nt-userservice-db", sqlServerPassword)
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithEnvironment("MSSQL_SA_PASSWORD", "Admin123")
            .WithHttpEndpoint(port: 1433, targetPort: 1433, isProxied: true);

var authServiceInstances = new List<IResourceBuilder<ProjectResource>>();
for (int port = 8101; port <= 8105; port++)
{
    authServiceInstances.Add(builder.AddProject<Projects.AuthService_Api>($"nt-authservice-service{port}")
            .WithEnvironment("RUNNING_WITH", "aspire")
            .WithReference(postgres)
            .WaitFor(postgres)
            .WithHttpEndpoint(port, name: $"http{port}")
                        .WithEnvironment("RabbitMqSettings__uri", "localhost")
            .WithEnvironment("RabbitMqSettings__username", "ntuser")
            .WithEnvironment("RabbitMqSettings__password", "pass")
            .WaitFor(consulServiceDiscovery)
            .WaitFor(rabbitmq)
            .WithUrls(c => c.Urls.ForEach(u => u.DisplayText = $"Open API")));
}

var authServiceLoadBalancer = builder.AddContainer("nt-authservice-loadbalancer", "nginx:latest")
            .WithHttpEndpoint(port: 8100, targetPort: 80)
            .WithBindMount(@"D:\Source\nt\server\nt.microservice\services\AuthService\AuthService.LoadBalancer\nginx\nginx.conf", "/etc/nginx/nginx.conf", isReadOnly: true)
            .WaitForAll(authServiceInstances);

var authServiceSideCar = builder.AddProject<Projects.AuthService_LoadBalancer_ServiceDiscoverySideCar>("AuthService-LoadBalancer-Sidecar")
            .WithEnvironment("ConsulConfig__serviceName", "nt.authservice.loadbalancer")
            .WithEnvironment("ConsulConfig__serviceId", "authservice-1")
            .WithEnvironment("ConsulConfig__serviceAddress", $"localhost")
            .WithEnvironment("ConsulConfig__servicePort", "8100")
            .WithEnvironment("ConsulConfig__healthCheckUrl", "http://host.docker.internal:8100/health")
            .WithEnvironment("ConsulConfig__consulAddress", consulServiceDiscovery.GetEndpoint("http"))
            .WithEnvironment("ConsulConfig__deregisterAfterMinutes", "5")
            .WaitFor(consulServiceDiscovery)
            .WaitFor(rabbitmq)
            .WaitFor(authServiceLoadBalancer);


var aggregatorService = builder.AddProject<Projects.UserIdentityAggregatorService_Api>(Constants.Infrastructure.AggregatorUserIdentityService.ServiceName, launchProfileName: Constants.Gateway.LaunchProfile)
            .WithEnvironment("ServiceDiscoveryOptions__ResolverName", "localhost")
            .WithEnvironment("ServiceDiscoveryOptions__ResolverPort", Constants.Infrastructure.Consul.HttpPort.ToString())
            .WithEnvironment("ServiceDiscoveryOptions__Services__0__Key", "UserService")
            .WithEnvironment("ServiceDiscoveryOptions__Services__0__Name", "nt.userservice.service")
            .WithEnvironment("ServiceDiscoveryOptions__Services__1__Key", "AuthService")
            .WithEnvironment("ServiceDiscoveryOptions__Services__1__Name", "nt.authservice.loadbalancer")
            .WaitFor(consulServiceDiscovery);

var gateway = builder.AddProject<Projects.nt_gateway>(Constants.Gateway.ServiceName, launchProfileName: Constants.Gateway.LaunchProfile)
           .WithEnvironment(Constants.Global.EnvironmentVariables.RunningWithVariable, Constants.Global.EnvironmentVariables.RunningWithValue)
           .WithEnvironment(Constants.Global.EnvironmentVariables.HostVariable, Constants.Global.EnvironmentVariables.HostValue);


var userService = builder.AddProject<Projects.UserService_Api>("nt-userservice-service")
            .WithEnvironment("RUNNING_WITH", "aspire")
            .WithEnvironment("RabbitMqSettings__host", "localhost")
            .WithEnvironment("RabbitMqSettings__username", "ntuser")
            .WithEnvironment("RabbitMqSettings__password", "pass")
            .WithEnvironment("ConsulConfig__serviceName", "nt.userservice.service")
            .WithEnvironment("ConsulConfig__serviceId", "userservice-1")
            .WithEnvironment("ConsulConfig__serviceAddress", $"localhost")
            .WithEnvironment("ConsulConfig__servicePort", "8301")
            .WithEnvironment("ConsulConfig__healthCheckUrl", "http://host.docker.internal:8301/api/healthcheck/health")
            .WithEnvironment("ConsulConfig__consulAddress", consulServiceDiscovery.GetEndpoint("http"))
            .WithEnvironment("ConsulConfig__deregisterAfterMinutes", "5")
            .WithHttpEndpoint(8301, name: "http")
            .WaitFor(blobStorage)
            .WithReference(sqlDb)
            .WaitFor(sqlDb)
            .WaitFor(consulServiceDiscovery)
            .WithUrls(c => c.Urls.ForEach(u => u.DisplayText = $"Open API ({u.Endpoint?.EndpointName})"));


var movieService = builder.AddProject<Projects.MovieService_Api>("nt-movieservice-service")
        .WithEnvironment("MovieDatabase__DatabaseName", "ntmoviestore")
        .WithEnvironment("MovieDatabase__MovieCollectionName", "movies")
        .WithReference(mongoDb)
        .WaitFor(mongoDb);

var reviewService = builder.AddProject<Projects.ReviewService_Api>("nt-reviewservice-service")
        .WithUrls(c => c.Urls.ForEach(u => u.DisplayText = $"Open API ({u.Endpoint?.EndpointName})"));


builder.Build().Run();
