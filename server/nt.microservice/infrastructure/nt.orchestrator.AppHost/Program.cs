var builder = DistributedApplication.CreateBuilder(args);

builder.AddGateway();
builder.AddUserIdentityAggregatorService();

var consul = builder.AddContainer("consul", "hashicorp/consul:latest")
    .WithContainerName("nt.common.servicediscovery")
    .WithHttpEndpoint(port:9500, targetPort:8500)
    .WithArgs("agent", "-dev", "-client=0.0.0.0"); // dev mode

builder.AddAuthService()
       .WaitFor(consul);

//builder.AddMovieService();
builder.AddUserService()
       .WaitFor(consul);

//builder.AddReviewService();


builder.Build().Run();
