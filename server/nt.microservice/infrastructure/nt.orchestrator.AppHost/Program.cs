using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Grpc.Net.Client.Balancer;
using static nt.helper.Constants.Infrastructure;


var root = @"D:\Source\nt\server\nt.microservice";
var builder = DistributedApplication.CreateBuilder(args);

var consulServiceDiscovery = builder.AddContainer(Constants.Infrastructure.Consul.ServiceName, "hashicorp/consul:latest")
            .WithContainerName(Constants.Infrastructure.Consul.ContainerName)
            .WithHttpEndpoint(port: Constants.Infrastructure.Consul.HttpPort, targetPort: Constants.Infrastructure.Consul.Port)
            .WithArgs("agent", "-dev", "-client=0.0.0.0"); // dev mode

// TODO: User Proper secret handling for RabbitMQ credentials
var rabbitMqusername = builder.AddParameter(Constants.Infrastructure.RabbitMq.UserNameKey, "ntuser", secret: true);
var rabbitMqpassword = builder.AddParameter(Constants.Infrastructure.RabbitMq.PasswordKey, "pass", secret: true);
var rabbitmq = builder.AddRabbitMQ(Constants.Infrastructure.RabbitMq.ServiceName, rabbitMqusername, rabbitMqpassword)
            .WithContainerName(Constants.Infrastructure.RabbitMq.ContainerName)
            .WithBindMount(source: @$"{root}/services/transportservices/rabbitmq-enabled-plugins",
                           target: @"/etc/rabbitmq/enabled_plugins")
            .WithBindMount(source: @$"{root}/services/transportservices/rabbitmq.config",
                           target: @"/etc/rabbitmq/rabbitmq.conf")
            .WithBindMount(source: @$"{root}/services/transportservices/rabbitmq-defs.json",
                           target: @"/etc/rabbitmq/definitions.json")
            .WithHttpEndpoint(5672,targetPort:5672, name:"http1")
            .WithHttpEndpoint(15672,targetPort:15672, name: "http2")
            .WithUrls(c => c.Urls.ForEach(u => u.DisplayText = $"Manage ({u.Endpoint?.EndpointName})")); ;



var postgresUsername = builder.AddParameter("postgresUser", "postgres", secret: true);
var postgresPassword = builder.AddParameter("postgressPassword", "Admin123", secret: true);

var postgres = builder.AddPostgres("UserSqlDb", postgresUsername, postgresPassword)
            .WithContainerName("nt.authservice.db")
            .WithImage("postgres:14.1-alpine")
            .WithPgAdmin()
            .WithDataVolume()
            .WithInitBindMount(@"D:\Source\nt\server\nt.microservice\services\Db\scripts\Aspire");

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

builder.AddProject<Projects.nt_gateway>(Constants.Gateway.ServiceName, launchProfileName: Constants.Gateway.LaunchProfile)
           .WithEnvironment(Constants.Global.EnvironmentVariables.RunningWithVariable, Constants.Global.EnvironmentVariables.RunningWithValue)
           .WithEnvironment(Constants.Global.EnvironmentVariables.HostVariable, Constants.Global.EnvironmentVariables.HostValue);


builder.AddUserService(rabbitmq)
        .WithEnvironment("ConsulConfig__serviceName", "nt.userservice.service")
        .WithEnvironment("ConsulConfig__serviceId", "userservice-1")
        .WithEnvironment("ConsulConfig__serviceAddress", $"localhost")
        .WithEnvironment("ConsulConfig__servicePort", "8301")
        .WithEnvironment("ConsulConfig__healthCheckUrl", "http://host.docker.internal:8301/api/healthcheck/health")
        .WithEnvironment("ConsulConfig__consulAddress", consulServiceDiscovery.GetEndpoint("http"))
        .WithEnvironment("ConsulConfig__deregisterAfterMinutes", "5")
        .WaitFor(consulServiceDiscovery)
        .WithUrls(c => c.Urls.ForEach(u => u.DisplayText = $"Open API ({u.Endpoint?.EndpointName})")); 


builder.AddMovieService();
//builder.AddReviewService();


builder.Build().Run();
