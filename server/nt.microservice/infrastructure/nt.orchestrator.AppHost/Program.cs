var root = @"D:\Source\nt\server\nt.microservice";
var builder = DistributedApplication.CreateBuilder(args);


var consulServiceDiscovery = builder.AddContainer(Constants.Infrastructure.Consul.ServiceName, "hashicorp/consul:latest")
                    .WithContainerName(Constants.Infrastructure.Consul.ContainerName)
                    .WithHttpEndpoint(port: Constants.Infrastructure.Consul.HttpPort, targetPort: Constants.Infrastructure.Consul.Port)
                    .WithArgs("agent", "-dev", "-client=0.0.0.0"); // dev mode

var aggregatorService = builder.AddProject<Projects.UserIdentityAggregatorService_Api>(Constants.Infrastructure.AggregatorUserIdentityService.ServiceName, launchProfileName: Constants.Gateway.LaunchProfile)
                               .WaitFor(consulServiceDiscovery);

builder.AddProject<Projects.nt_gateway>(Constants.Gateway.ServiceName, launchProfileName: Constants.Gateway.LaunchProfile)
       .WithEnvironment(Constants.Global.EnvironmentVariables.RunningWithVariable, Constants.Global.EnvironmentVariables.RunningWithValue)
       .WithEnvironment(Constants.Global.EnvironmentVariables.HostVariable, Constants.Global.EnvironmentVariables.HostValue);



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
    .WithHttpEndpoint(15672,targetPort:15672, name: "http2");


builder.AddAuthService(consulServiceDiscovery, rabbitmq)
       .WaitFor(consulServiceDiscovery);


builder.AddUserService(rabbitmq)
       .WaitFor(consulServiceDiscovery);

builder.AddMovieService();
//builder.AddReviewService();


builder.Build().Run();
