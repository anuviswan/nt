using Aspire.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using nt.orchestrator.AppHost.Settings;

var hostBuilder = Host.CreateApplicationBuilder(args);
var config = hostBuilder.Configuration;
var infrastructureSettings = config.GetSection(nameof(InfrastructureSettings)).Get<InfrastructureSettings>();
var serviceSettings = config.GetSection(nameof(ServicesSettings)).Get<ServicesSettings>();

ArgumentNullException.ThrowIfNull(infrastructureSettings, nameof(infrastructureSettings));
ArgumentNullException.ThrowIfNull(serviceSettings, nameof(serviceSettings));

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
            .WithPgAdmin(p=>p.WithHostPort(5000))
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
            .WithVolume("//d/Source/nt/server/nt.microservice/services/UserService/BlobStorage:/data")
            .WithArgs("azurite-blob", "--blobHost", "0.0.0.0", "-l", "/data")
            .WithHttpEndpoint(port: infrastructureSettings.BlobStorage.HostPort, targetPort: infrastructureSettings.BlobStorage.TargetPort, isProxied: true);


var sqlServerPassword = builder.AddParameter(Constants.UserService.Database.PasswordKey, infrastructureSettings.SqlServer.Password);
var sqlDb = builder.AddSqlServer(Constants.UserService.Database.InstanceName, sqlServerPassword)
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithEnvironment("MSSQL_SA_PASSWORD", infrastructureSettings.SqlServer.Password)
            .WithHttpEndpoint(port: infrastructureSettings.SqlServer.HostPort, targetPort: infrastructureSettings.SqlServer.TargetPort, isProxied: true);


var authServiceInstances = new List<IResourceBuilder<ProjectResource>>();
foreach(var port in serviceSettings.AuthService.InstancePorts)
{
    authServiceInstances.Add(builder.AddProject<Projects.AuthService_Api>($"{Constants.AuthService.ServiceName}{port}")
            .WithEnvironment(Constants.Global.EnvironmentVariables.RunningWithVariable, Constants.Global.EnvironmentVariables.RunningWithValue)
            .WithReference(postgres)
            .WaitFor(postgres)
            .WithHttpEndpoint(port, name: $"http{port}")
            .WithEnvironment(Constants.AuthService.Environment.RabbitMqHost, infrastructureSettings.RabbitMq.Host)
            .WithEnvironment(Constants.AuthService.Environment.RabbitMqUserName, infrastructureSettings.RabbitMq.UserName)
            .WithEnvironment(Constants.AuthService.Environment.RabbitMqPassword, infrastructureSettings.RabbitMq.Password)
            .WaitFor(consulServiceDiscovery)
            .WaitFor(rabbitmq)
            .WithUrls(c => c.Urls.ForEach(u => u.DisplayText = $"Open API")));
}

var authServiceLoadBalancer = builder.AddContainer(Constants.AuthService.LoadBalancer.InstanceName, serviceSettings.AuthService.LoadBalancer.DockerImage)
            .WithHttpEndpoint(port: serviceSettings.AuthService.LoadBalancer.HostPort, targetPort: serviceSettings.AuthService.LoadBalancer.TargetPort)
            .WithBindMount($@"{root}\services\AuthService\AuthService.LoadBalancer\nginx\nginx.conf", "/etc/nginx/nginx.conf", isReadOnly: true)
            .WaitForAll(authServiceInstances);

var authServiceSideCar = builder.AddProject<Projects.AuthService_LoadBalancer_ServiceDiscoverySideCar>(Constants.AuthService.Sidecar.InstanceName)
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceName, serviceSettings.AuthService.ConsulSideCar.ServiceName)
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceId, serviceSettings.AuthService.ConsulSideCar.ServiceId)
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceAddress, serviceSettings.AuthService.ConsulSideCar.ServiceAddress)
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServicePort, serviceSettings.AuthService.ConsulSideCar.ServicePort.ToString())
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceHealthCheckUrl, serviceSettings.AuthService.ConsulSideCar.HealthCheckUrl)
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ConsulAddress, consulServiceDiscovery.GetEndpoint("http"))
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.DeregisterAfter, serviceSettings.AuthService.ConsulSideCar.DeregisterAfterMinutes.ToString())
            .WaitFor(consulServiceDiscovery)
            .WaitFor(rabbitmq)
            .WaitFor(authServiceLoadBalancer);


var userService = builder.AddProject<Projects.UserService_Api>(Constants.UserService.ServiceName)
            .WithEnvironment(Constants.Global.EnvironmentVariables.RunningWithVariable, Constants.Global.EnvironmentVariables.RunningWithValue)
            .WithEnvironment(Constants.UserService.Environment.RabbitMqHost, infrastructureSettings.RabbitMq.Host)
            .WithEnvironment(Constants.UserService.Environment.RabbitMqUserName, infrastructureSettings.RabbitMq.UserName)
            .WithEnvironment(Constants.UserService.Environment.RabbitMqPassword, infrastructureSettings.RabbitMq.Password)
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceName, serviceSettings.UserService.ServiceDiscovery.ServiceName)
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceId, serviceSettings.UserService.ServiceDiscovery.ServiceId)
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceAddress, serviceSettings.UserService.ServiceDiscovery.ServiceAddress)
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServicePort, serviceSettings.UserService.ServiceDiscovery.ServicePort.ToString())
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceHealthCheckUrl, serviceSettings.UserService.ServiceDiscovery.HealthCheckUrl)
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ConsulAddress, consulServiceDiscovery.GetEndpoint("http"))
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.DeregisterAfter, serviceSettings.UserService.ServiceDiscovery.DeregisterAfterMinutes.ToString())
            .WithHttpEndpoint(8301, name: "http")
            .WaitFor(blobStorage)
            .WithReference(sqlDb)
            .WaitFor(sqlDb)
            .WaitFor(consulServiceDiscovery)
            .WithUrls(c => c.Urls.ForEach(u => u.DisplayText = $"Open API ({u.Endpoint?.EndpointName})"));

var aggregatorService = builder.AddProject<Projects.UserIdentityAggregatorService_Api>(Constants.AggregatorUserIdentityService.ServiceName, launchProfileName: Constants.Global.Common.LaunchProfile)
            .WithEnvironment(Constants.Global.EnvironmentVariables.RunningWithVariable, Constants.Global.EnvironmentVariables.RunningWithValue)
            .WithEnvironment(Constants.AggregatorUserIdentityService.ServiceDiscoveryResolverName, serviceSettings.AggregateAuthUserService.ServiceDiscoveryOptions.ResolverName)
            .WithEnvironment(Constants.AggregatorUserIdentityService.ServiceDiscoveryResolverPort, infrastructureSettings.Consul.HostPort.ToString())
            .WithEnvironment(Constants.AggregatorUserIdentityService.ServiceDiscoveryUserServiceKey, serviceSettings.AggregateAuthUserService.ServiceDiscoveryOptions.Services.Single(x => x.Key == "UserService").Key)
            .WithEnvironment(Constants.AggregatorUserIdentityService.ServiceDiscoveryUserServiceName, serviceSettings.AggregateAuthUserService.ServiceDiscoveryOptions.Services.Single(x => x.Key == "UserService").Name)
            .WithEnvironment(Constants.AggregatorUserIdentityService.ServiceDiscoveryAuthServiceKey, serviceSettings.AggregateAuthUserService.ServiceDiscoveryOptions.Services.Single(x => x.Key == "AuthService").Key)
            .WithEnvironment(Constants.AggregatorUserIdentityService.ServiceDiscoveryAuthServiceName, serviceSettings.AggregateAuthUserService.ServiceDiscoveryOptions.Services.Single(x => x.Key == "AuthService").Name)
            .WaitFor(consulServiceDiscovery)
            .WaitFor(authServiceLoadBalancer)
            .WaitFor(authServiceSideCar)
            .WaitFor(userService);


var movieService = builder.AddProject<Projects.MovieService_Api>(Constants.MovieService.ServiceName)
        .WithEnvironment(Constants.Global.EnvironmentVariables.RunningWithVariable, Constants.Global.EnvironmentVariables.RunningWithValue)
        .WithEnvironment(Constants.MovieService.EnvironmentVariable.DbName, serviceSettings.MovieService.DbName)
        .WithEnvironment(Constants.MovieService.EnvironmentVariable.DbCollection, serviceSettings.MovieService.MovieCollectionName)
        .WithReference(mongoDb)
        .WaitFor(mongoDb);

var reviewService = builder.AddProject<Projects.ReviewService_Api>("nt-reviewservice-service")
        .WithEnvironment(Constants.Global.EnvironmentVariables.RunningWithVariable, Constants.Global.EnvironmentVariables.RunningWithValue)
        .WithUrls(c => c.Urls.ForEach(u => u.DisplayText = $"Open API ({u.Endpoint?.EndpointName})"));


var gateway = builder.AddProject<Projects.nt_gateway>(Constants.Gateway.ServiceName, launchProfileName: Constants.Gateway.LaunchProfile)
           .WithEnvironment(Constants.Global.EnvironmentVariables.RunningWithVariable, Constants.Global.EnvironmentVariables.RunningWithValue)
           .WithEnvironment(Constants.Global.EnvironmentVariables.HostVariable, serviceSettings.Gateway.Host)
           .WaitForAll(authServiceInstances)
           .WaitFor(userService)
           .WaitFor(aggregatorService)
           .WaitFor(movieService)
           .WaitFor(reviewService);


builder.Build().Run();
