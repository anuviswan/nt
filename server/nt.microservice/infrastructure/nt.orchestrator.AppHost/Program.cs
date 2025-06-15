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
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceName, serviceSettings.AuthService.ServiceRegistrationConfig.ServiceName)
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceId, serviceSettings.AuthService.ServiceRegistrationConfig.ServiceId)
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceHost, serviceSettings.AuthService.ServiceRegistrationConfig.ServiceHost)
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServicePort, serviceSettings.AuthService.ServiceRegistrationConfig.ServicePort.ToString())
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceHealthCheckUrl, serviceSettings.AuthService.ServiceRegistrationConfig.HealthCheckUrl)
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.RegistryUri, consulServiceDiscovery.GetEndpoint("http"))
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.DeregisterAfter, serviceSettings.AuthService.ServiceRegistrationConfig.DeregisterAfterMinutes.ToString())
            .WaitFor(consulServiceDiscovery)
            .WaitFor(rabbitmq)
            .WaitFor(authServiceLoadBalancer);


var userService = builder.AddProject<Projects.UserService_Api>(Constants.UserService.ServiceName)
            .WithEnvironment(Constants.Global.EnvironmentVariables.RunningWithVariable, Constants.Global.EnvironmentVariables.RunningWithValue)
            .WithEnvironment(Constants.UserService.Environment.RabbitMqHost, infrastructureSettings.RabbitMq.Host)
            .WithEnvironment(Constants.UserService.Environment.RabbitMqUserName, infrastructureSettings.RabbitMq.UserName)
            .WithEnvironment(Constants.UserService.Environment.RabbitMqPassword, infrastructureSettings.RabbitMq.Password)
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceName, serviceSettings.UserService.ServiceRegistrationConfig.ServiceName)
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceId, serviceSettings.UserService.ServiceRegistrationConfig.ServiceId)
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceHost, serviceSettings.UserService.ServiceRegistrationConfig.ServiceHost)
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.RegistryUri, consulServiceDiscovery.GetEndpoint("http"))
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.DeregisterAfter, serviceSettings.UserService.ServiceRegistrationConfig.DeregisterAfterMinutes.ToString())
            .WithEnvironment("BlobConfig__ConnectionString", infrastructureSettings.BlobStorage.blobConfig.ConnectionString)
            .WithHttpEndpoint(name:"http")
            .WaitFor(blobStorage)
            .WithReference(sqlDb)
            .WaitFor(sqlDb)
            .WaitFor(consulServiceDiscovery)
            .WithUrls(c => c.Urls.ForEach(u => u.DisplayText = $"Open API ({u.Endpoint?.EndpointName})"));


userService.WithEnvironment(Constants.Infrastructure.Consul.Environement.ServicePort, ()=>userService.GetEndpoint("http").Port.ToString())
           .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceHealthCheckUrl, 
                            ()=> $"http://host.docker.internal:{userService.GetEndpoint("http").Port}{serviceSettings.UserService.ServiceRegistrationConfig.HealthCheckUrl}");

var aggregatorService = builder.AddProject<Projects.UserIdentityAggregatorService_Api>(Constants.AggregatorUserIdentityService.ServiceName)
            .WithEnvironment(Constants.Global.EnvironmentVariables.RunningWithVariable, Constants.Global.EnvironmentVariables.RunningWithValue)
            .WithEnvironment(Constants.AggregatorUserIdentityService.ServiceMapping_RegistryUri, consulServiceDiscovery.GetEndpoint("http"))
            .WithEnvironment(Constants.AggregatorUserIdentityService.ServiceMappingUserServiceKey, serviceSettings.AggregateAuthUserService.ServiceMappingConfig.Services.Single(x => x.Key == "UserService").Key)
            .WithEnvironment(Constants.AggregatorUserIdentityService.ServiceMappingUserServiceName, serviceSettings.AggregateAuthUserService.ServiceMappingConfig.Services.Single(x => x.Key == "UserService").Name)
            .WithEnvironment(Constants.AggregatorUserIdentityService.ServiceMappingAuthServiceKey, serviceSettings.AggregateAuthUserService.ServiceMappingConfig.Services.Single(x => x.Key == "AuthService").Key)
            .WithEnvironment(Constants.AggregatorUserIdentityService.ServiceMappingAuthServiceName, serviceSettings.AggregateAuthUserService.ServiceMappingConfig.Services.Single(x => x.Key == "AuthService").Name)
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceName, serviceSettings.AggregateAuthUserService.ServiceRegistrationConfig.ServiceName)
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceId, serviceSettings.AggregateAuthUserService.ServiceRegistrationConfig.ServiceId)
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceHost, serviceSettings.AggregateAuthUserService.ServiceRegistrationConfig.ServiceHost)
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.RegistryUri, consulServiceDiscovery.GetEndpoint("http"))
            .WithEnvironment(Constants.Infrastructure.Consul.Environement.DeregisterAfter, serviceSettings.AggregateAuthUserService.ServiceRegistrationConfig.DeregisterAfterMinutes.ToString())
            //.WithHttpEndpoint(name: "http")
            .WaitFor(consulServiceDiscovery)
            .WaitFor(authServiceLoadBalancer)
            .WaitFor(authServiceSideCar)
            .WaitFor(userService);

aggregatorService.WithEnvironment(Constants.Infrastructure.Consul.Environement.ServicePort, () => aggregatorService.GetEndpoint("http").Port.ToString())
           .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceHealthCheckUrl,
                            () => $"http://host.docker.internal:{aggregatorService.GetEndpoint("http").Port}{serviceSettings.AggregateAuthUserService.ServiceRegistrationConfig.HealthCheckUrl}");

var movieService = builder.AddProject<Projects.MovieService_Api>(Constants.MovieService.ServiceName)
        .WithEnvironment(Constants.Global.EnvironmentVariables.RunningWithVariable, Constants.Global.EnvironmentVariables.RunningWithValue)
        .WithEnvironment(Constants.MovieService.EnvironmentVariable.DbName, serviceSettings.MovieService.DbName)
        .WithEnvironment(Constants.MovieService.EnvironmentVariable.DbCollection, serviceSettings.MovieService.MovieCollectionName)
        .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceName, serviceSettings.MovieService.ServiceRegistrationConfig.ServiceName)
        .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceId, serviceSettings.MovieService.ServiceRegistrationConfig.ServiceId)
        .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceHost, serviceSettings.MovieService.ServiceRegistrationConfig.ServiceHost)
        .WithEnvironment(Constants.Infrastructure.Consul.Environement.RegistryUri, consulServiceDiscovery.GetEndpoint("http"))
        .WithEnvironment(Constants.Infrastructure.Consul.Environement.DeregisterAfter, serviceSettings.MovieService.ServiceRegistrationConfig.DeregisterAfterMinutes.ToString())
        .WithReference(mongoDb)
        .WaitFor(mongoDb);

movieService.WithEnvironment(Constants.Infrastructure.Consul.Environement.ServicePort, () => movieService.GetEndpoint("http").Port.ToString())
           .WithEnvironment(Constants.Infrastructure.Consul.Environement.ServiceHealthCheckUrl,
                            () => $"http://host.docker.internal:{movieService.GetEndpoint("http").Port}{serviceSettings.MovieService.ServiceRegistrationConfig.HealthCheckUrl}");

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
