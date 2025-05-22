var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.nt_gateway>("nt-gateway");

builder.Build().Run();
