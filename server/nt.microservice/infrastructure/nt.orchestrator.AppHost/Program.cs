using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;

var root = @"D:\Source\nt\server\nt.microservice";
var builder = DistributedApplication.CreateBuilder(args);

builder.AddGateway();
builder.AddUserIdentityAggregatorService();

var consul = builder.AddContainer("consul", "hashicorp/consul:latest")
    .WithContainerName("nt.common.servicediscovery")
    .WithHttpEndpoint(port:9500, targetPort:8500)
    .WithArgs("agent", "-dev", "-client=0.0.0.0"); // dev mode


var rabbitMqusername = builder.AddParameter("rabbitMqusername", "ntuser", secret: true);
var rabbitMqpassword = builder.AddParameter("rabbitMqpassword", "pass", secret: true);

var rabbitmq = builder.AddRabbitMQ("nt-common-rabbitmq", rabbitMqusername, rabbitMqpassword)
    .WithBindMount(source: @$"{root}/services/transportservices/rabbitmq-enabled-plugins",
                   target: @"/etc/rabbitmq/enabled_plugins")
    .WithBindMount(source: @$"{root}/services/transportservices/rabbitmq.config",
                   target: @"/etc/rabbitmq/rabbitmq.conf")
    .WithBindMount(source: @$"{root}/services/transportservices/rabbitmq-defs.json",
                   target: @"/etc/rabbitmq/definitions.json")
    .WithHttpEndpoint(5672,targetPort:5672, name:"http1")
    .WithHttpEndpoint(15672,targetPort:15672, name: "http2");


builder.AddAuthService(consul, rabbitmq)
       .WaitFor(consul);

//builder.AddMovieService();
builder.AddUserService(rabbitmq)
       .WaitFor(consul);

//builder.AddReviewService();


builder.Build().Run();
