using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.nt_gateway>("nt-gateway");

builder.AddProject<Projects.UserIdentityAggregatorService_Api>("useridentityaggregatorservice-api");

builder.AddProject<Projects.AuthService_Api>("authservice-api");

builder.AddProject<Projects.MovieService_Api>("movieservice-api");

builder.AddProject<Projects.UserService_Api>("userservice-api");

builder.AddProject<Projects.ReviewService_Api>("reviewservice-api");
builder.Build().Run();
