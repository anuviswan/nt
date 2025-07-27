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
            .WithArgs("agent", "-dev", "-client=0.0.0.0") // dev mode
            .WithUrls(c => c.Urls.ForEach(u => u.DisplayText = $"Dashboard ({u.Endpoint?.Port})")); 


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
            .WithUrls(c => c.Urls.ForEach(u => u.DisplayText = $"Dashboard ({u.Endpoint?.Port})")); 


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

var mongoDbMovie = builder.AddMongoDB(Constants.MovieService.Database.InstanceName, 27017, userName: mongoDbUsername, password: mongoDbPassword)
            .WithEnvironment(Constants.MovieService.EnvironmentVariable.DbUserNameKey, infrastructureSettings.MongoDb.UserName)
            .WithEnvironment(Constants.MovieService.EnvironmentVariable.DbPasswordKey, infrastructureSettings.MongoDb.Password)
            //.WithEndpoint(port: 27017, targetPort: 27017, isProxied: true)
            .WithContainerName(Constants.MovieService.Database.ContainerName)
            .WithDataVolume()
            .WithMongoExpress();

var mongoDbReview = builder.AddMongoDB(Constants.ReviewService.Database.InstanceName, 27018, userName: mongoDbUsername, password: mongoDbPassword)
            .WithEnvironment(Constants.ReviewService.EnvironmentVariable.DbUserNameKey, infrastructureSettings.MongoDb.UserName)
            .WithEnvironment(Constants.ReviewService.EnvironmentVariable.DbPasswordKey, infrastructureSettings.MongoDb.Password)
            //.WithEndpoint(port: 27017, targetPort: 27017, isProxied: true)
            .WithContainerName(Constants.ReviewService.Database.ContainerName)
            .WithDataVolume()
            .WithMongoExpress();

var redisPassword = builder.AddParameter(Constants.ReviewService.Cache.PasswordKey, "Password123", secret: true);
var redisReview = builder.AddRedis(Constants.ReviewService.Cache.InstanceName, port: 6379, password: redisPassword)
            .WithEnvironment("Redis__Host", Constants.ReviewService.Cache.InstanceName) // Set in your app
            .WithEnvironment("Redis__Port", "6379")
            .WithContainerName(Constants.ReviewService.Cache.ContainerName)
            .WithHttpEndpoint(port: 8081, targetPort: 8081, isProxied: true)
            .WithRedisInsight()
            .WithRedisCommander();

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
            .WithUrls(c => c.Urls.ForEach(u => u.DisplayText = $"Open API ({u.Endpoint?.Port})")));
}

var authServiceLoadBalancer = builder.AddContainer(Constants.AuthService.LoadBalancer.InstanceName, serviceSettings.AuthService.LoadBalancer.DockerImage)
            .WithHttpEndpoint(port: serviceSettings.AuthService.LoadBalancer.HostPort, targetPort: serviceSettings.AuthService.LoadBalancer.TargetPort)
            .WithBindMount($@"{root}\services\AuthService\AuthService.LoadBalancer\nginx\nginx.conf", "/etc/nginx/nginx.conf", isReadOnly: true)
            .WaitForAll(authServiceInstances);

var authServiceSideCar = builder.AddProject<Projects.AuthService_LoadBalancer_ServiceDiscoverySideCar>(Constants.AuthService.Sidecar.InstanceName)
            .WithEnvironment(Constants.Infrastructure.Consul.Environment.ServiceName, serviceSettings.AuthService.ServiceRegistrationConfig.ServiceName)
            .WithEnvironment(Constants.Infrastructure.Consul.Environment.ServiceId, serviceSettings.AuthService.ServiceRegistrationConfig.ServiceId)
            .WithEnvironment(Constants.Infrastructure.Consul.Environment.ServiceHost, serviceSettings.AuthService.ServiceRegistrationConfig.ServiceHost)
            .WithEnvironment(Constants.Infrastructure.Consul.Environment.ServicePort, serviceSettings.AuthService.ServiceRegistrationConfig.ServicePort.ToString())
            .WithEnvironment(Constants.Infrastructure.Consul.Environment.ServiceHealthCheckUrl, serviceSettings.AuthService.ServiceRegistrationConfig.HealthCheckUrl)
            .WithEnvironment(Constants.Infrastructure.Consul.Environment.RegistryUri, consulServiceDiscovery.GetEndpoint("http"))
            .WithEnvironment(Constants.Infrastructure.Consul.Environment.DeregisterAfter, serviceSettings.AuthService.ServiceRegistrationConfig.DeregisterAfterMinutes.ToString())
            .WaitFor(consulServiceDiscovery)
            .WaitFor(rabbitmq)
            .WaitFor(authServiceLoadBalancer);


var userService = builder.AddProject<Projects.UserService_Api>(Constants.UserService.ServiceName)
            .WithEnvironment(Constants.Global.EnvironmentVariables.RunningWithVariable, Constants.Global.EnvironmentVariables.RunningWithValue)
            .WithEnvironment(Constants.UserService.Environment.RabbitMqHost, infrastructureSettings.RabbitMq.Host)
            .WithEnvironment(Constants.UserService.Environment.RabbitMqUserName, infrastructureSettings.RabbitMq.UserName)
            .WithEnvironment(Constants.UserService.Environment.RabbitMqPassword, infrastructureSettings.RabbitMq.Password)
            .WithEnvironment(Constants.Infrastructure.Consul.Environment.ServiceName, serviceSettings.UserService.ServiceRegistrationConfig.ServiceName)
            .WithEnvironment(Constants.Infrastructure.Consul.Environment.ServiceId, serviceSettings.UserService.ServiceRegistrationConfig.ServiceId)
            .WithEnvironment(Constants.Infrastructure.Consul.Environment.ServiceHost, serviceSettings.UserService.ServiceRegistrationConfig.ServiceHost)
            .WithEnvironment(Constants.Infrastructure.Consul.Environment.RegistryUri, consulServiceDiscovery.GetEndpoint("http"))
            .WithEnvironment(Constants.Infrastructure.Consul.Environment.DeregisterAfter, serviceSettings.UserService.ServiceRegistrationConfig.DeregisterAfterMinutes.ToString())
            .WithEnvironment("BlobConfig__ConnectionString", infrastructureSettings.BlobStorage.blobConfig.ConnectionString)
            .WithHttpEndpoint(name:"http")
            .WaitFor(blobStorage)
            .WithReference(sqlDb)
            .WaitFor(sqlDb)
            .WaitFor(consulServiceDiscovery)
            .WithUrls(c => c.Urls.ForEach(u => u.DisplayText = $"Open API ({u.Endpoint?.Port})"));


userService.WithEnvironment(Constants.Infrastructure.Consul.Environment.ServicePort, ()=>userService.GetEndpoint("http").Port.ToString())
           .WithEnvironment(Constants.Infrastructure.Consul.Environment.ServiceHealthCheckUrl, 
                            ()=> $"http://host.docker.internal:{userService.GetEndpoint("http").Port}{serviceSettings.UserService.ServiceRegistrationConfig.HealthCheckUrl}");

var aggregatorService = builder.AddProject<Projects.UserIdentityAggregatorService_Api>(Constants.AggregatorUserIdentityService.ServiceName)
            .WithEnvironment(Constants.Global.EnvironmentVariables.RunningWithVariable, Constants.Global.EnvironmentVariables.RunningWithValue)
            .WithEnvironment(Constants.AggregatorUserIdentityService.ServiceMapping_RegistryUri, consulServiceDiscovery.GetEndpoint("http"))
            .WithEnvironment(Constants.AggregatorUserIdentityService.ServiceMappingUserServiceKey, serviceSettings.AggregateAuthUserService.ServiceMappingConfig.Services.Single(x => x.Key == "UserService").Key)
            .WithEnvironment(Constants.AggregatorUserIdentityService.ServiceMappingUserServiceName, serviceSettings.AggregateAuthUserService.ServiceMappingConfig.Services.Single(x => x.Key == "UserService").Name)
            .WithEnvironment(Constants.AggregatorUserIdentityService.ServiceMappingAuthServiceKey, serviceSettings.AggregateAuthUserService.ServiceMappingConfig.Services.Single(x => x.Key == "AuthService").Key)
            .WithEnvironment(Constants.AggregatorUserIdentityService.ServiceMappingAuthServiceName, serviceSettings.AggregateAuthUserService.ServiceMappingConfig.Services.Single(x => x.Key == "AuthService").Name)
            .WithEnvironment(Constants.Infrastructure.Consul.Environment.ServiceName, serviceSettings.AggregateAuthUserService.ServiceRegistrationConfig.ServiceName)
            .WithEnvironment(Constants.Infrastructure.Consul.Environment.ServiceId, serviceSettings.AggregateAuthUserService.ServiceRegistrationConfig.ServiceId)
            .WithEnvironment(Constants.Infrastructure.Consul.Environment.ServiceHost, serviceSettings.AggregateAuthUserService.ServiceRegistrationConfig.ServiceHost)
            .WithEnvironment(Constants.Infrastructure.Consul.Environment.RegistryUri, consulServiceDiscovery.GetEndpoint("http"))
            .WithEnvironment(Constants.Infrastructure.Consul.Environment.DeregisterAfter, serviceSettings.AggregateAuthUserService.ServiceRegistrationConfig.DeregisterAfterMinutes.ToString())
            //.WithHttpEndpoint(name: "http")
            .WaitFor(consulServiceDiscovery)
            .WaitFor(authServiceLoadBalancer)
            .WaitFor(authServiceSideCar)
            .WaitFor(userService)
            .WithUrls(c => c.Urls.ForEach(u => u.DisplayText = $"Open API ({u.Endpoint?.Port})")); ;

aggregatorService.WithEnvironment(Constants.Infrastructure.Consul.Environment.ServicePort, () => aggregatorService.GetEndpoint("http").Port.ToString())
           .WithEnvironment(Constants.Infrastructure.Consul.Environment.ServiceHealthCheckUrl,
                            () => $"http://host.docker.internal:{aggregatorService.GetEndpoint("http").Port}{serviceSettings.AggregateAuthUserService.ServiceRegistrationConfig.HealthCheckUrl}");

var movieService = builder.AddProject<Projects.MovieService_Api>(Constants.MovieService.ServiceName)
        .WithEnvironment(Constants.Global.EnvironmentVariables.RunningWithVariable, Constants.Global.EnvironmentVariables.RunningWithValue)
        .WithEnvironment(Constants.MovieService.EnvironmentVariable.DbName, serviceSettings.MovieService.DbName)
        .WithEnvironment(Constants.MovieService.EnvironmentVariable.DbCollection, serviceSettings.MovieService.MovieCollectionName)
        .WithEnvironment(Constants.MovieService.EnvironmentVariable.ServiceName, serviceSettings.MovieService.ServiceRegistrationConfig.ServiceName)
        .WithEnvironment(Constants.MovieService.EnvironmentVariable.ServiceId, serviceSettings.MovieService.ServiceRegistrationConfig.ServiceId)
        .WithEnvironment(Constants.MovieService.EnvironmentVariable.ServiceHost, serviceSettings.MovieService.ServiceRegistrationConfig.ServiceHost)
        .WithEnvironment(Constants.MovieService.EnvironmentVariable.RegistryUri, consulServiceDiscovery.GetEndpoint("http"))
        .WithEnvironment(Constants.MovieService.EnvironmentVariable.DeregisterAfter, serviceSettings.MovieService.ServiceRegistrationConfig.DeregisterAfterMinutes.ToString())
        .WithReference(mongoDbMovie)
        .WaitFor(mongoDbMovie)
        .WaitFor(consulServiceDiscovery)
        .WithUrls(c => c.Urls.ForEach(u => u.DisplayText = $"Open API ({u.Endpoint?.Port})")); ;

movieService.WithEnvironment(Constants.MovieService.EnvironmentVariable.ServicePort, () => movieService.GetEndpoint("http").Port.ToString())
           .WithEnvironment(Constants.MovieService.EnvironmentVariable.ServiceHealthCheckUrl,
                            () => $"http://host.docker.internal:{movieService.GetEndpoint("http").Port}{serviceSettings.MovieService.ServiceRegistrationConfig.HealthCheckUrl}");

var reviewService = builder.AddProject<Projects.ReviewService_Presenation_Api>(Constants.ReviewService.ServiceName)
        .WithEnvironment(Constants.Global.EnvironmentVariables.RunningWithVariable, Constants.Global.EnvironmentVariables.RunningWithValue)
        .WithEnvironment(Constants.ReviewService.EnvironmentVariable.DbName, serviceSettings.ReviewService.DbName)
        .WithEnvironment(Constants.ReviewService.EnvironmentVariable.DbCollection, serviceSettings.ReviewService.ReviewCollectionName)
        .WithEnvironment(Constants.ReviewService.EnvironmentVariable.ServiceName, serviceSettings.ReviewService.ServiceRegistrationConfig.ServiceName)
        .WithEnvironment(Constants.ReviewService.EnvironmentVariable.ServiceId, serviceSettings.ReviewService.ServiceRegistrationConfig.ServiceId)
        .WithEnvironment(Constants.ReviewService.EnvironmentVariable.ServiceHost, serviceSettings.ReviewService.ServiceRegistrationConfig.ServiceHost)
        .WithEnvironment(Constants.ReviewService.EnvironmentVariable.RegistryUri, consulServiceDiscovery.GetEndpoint("http"))
        .WithEnvironment(Constants.ReviewService.EnvironmentVariable.DeregisterAfter, serviceSettings.MovieService.ServiceRegistrationConfig.DeregisterAfterMinutes.ToString())
        .WithReference(mongoDbReview)
        .WaitFor(mongoDbReview)
        .WaitFor(redisReview)
        .WaitFor(consulServiceDiscovery)
        .WithReference(redisReview)
        .WithUrls(c => c.Urls.ForEach(u => u.DisplayText = $"Open API ({u.Endpoint?.EndpointName})"));

reviewService.WithEnvironment(Constants.ReviewService.EnvironmentVariable.ServicePort, () => movieService.GetEndpoint("http").Port.ToString())
           .WithEnvironment(Constants.ReviewService.EnvironmentVariable.ServiceHealthCheckUrl,
                            () => $"http://host.docker.internal:{reviewService.GetEndpoint("http").Port}{serviceSettings.ReviewService.ServiceRegistrationConfig.HealthCheckUrl}");

var gateway = builder.AddProject<Projects.nt_gateway>(Constants.Gateway.ServiceName, launchProfileName: Constants.Gateway.LaunchProfile)
           .WithEnvironment(Constants.Global.EnvironmentVariables.RunningWithVariable, Constants.Global.EnvironmentVariables.RunningWithValue)
           .WithEnvironment(Constants.Global.EnvironmentVariables.HostVariable, serviceSettings.Gateway.Host)
           .WaitForAll(authServiceInstances)
           .WaitFor(userService)
           .WaitFor(aggregatorService)
           .WaitFor(movieService)
           .WaitFor(reviewService)
           .WithUrls(c => c.Urls.ForEach(u => u.DisplayText = $"Open API ({u.Endpoint?.Port})")); ;


builder.Build().Run();
