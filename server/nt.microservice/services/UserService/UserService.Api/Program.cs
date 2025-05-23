using Consul;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.IdentityModel.Tokens;
using Prometheus;
using Serilog;
using Serilog.Formatting.Compact;
using System.Text;
using UserService.Api.ConsumerServices;
using UserService.Api.Controllers;
using UserService.Api.Settings;
using UserService.Service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
var rabbitMqSettings = builder.Configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();

var consulConfig = builder.Configuration.GetSection(nameof(ConsulConfig)).Get<ConsulConfig>();
ArgumentNullException.ThrowIfNull(consulConfig, nameof(consulConfig));

var corsPolicy = "_ntClientAppsOrigins";

builder.Services.AddCors(option => {
    option.AddPolicy(name: corsPolicy,
        builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        });
});

var logger = new LoggerConfiguration()
  .ReadFrom.AppSettings()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .WriteTo.File(new CompactJsonFormatter(), Path.Combine("Logs", "log.txt"), rollingInterval: RollingInterval.Day)
  .CreateLogger();


builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UserserviceDbContext>();

builder.Services.AddMediatR(typeof(CreateUserCommand).Assembly);
builder.Services.AddTransient(typeof(IGenericRepository<,>),typeof(GenericRepository<,>));
builder.Services.AddAutoMapper(typeof(UserController),typeof(CreateUserCommand));
builder.Services.AddTransient<IUserMetaInformationRepository,UserMetaInformationRepository>();

builder.Services.AddSingleton<IBlobHandlerService, BlobHandlerService>();
builder.Services.AddMassTransit(mt =>
                        {
                            mt.AddConsumersFromNamespaceContaining(typeof(CreateUserInitiatedSucceededConsumer));
                            mt.UsingRabbitMq((cntxt, cfg) =>
                            {

                                cfg.Host(rabbitMqSettings?.Host, "/", c =>
                                {
                                    c.Username(rabbitMqSettings?.UserName);
                                    c.Password(rabbitMqSettings?.Password);
                                });

                                cfg.ConfigureEndpoints(cntxt);
                            });
                            }
                        );

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        var jwt = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
        if ((jwt?.Validate()) != true)
        {
            throw new Exception("Unable to read Jwt Settings");
        }

        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Aud,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
        };
    });



var app = builder.Build();
app.Lifetime.ApplicationStarted.Register(() =>
{
    if (Environment.GetEnvironmentVariable("RUNNING_WITH")?.ToUpper() == "ASPIRE")
    {
        Console.WriteLine("Running with Aspire");
        consulConfig.ConsulAddress = Environment.GetEnvironmentVariable("CONSUL_URL") ?? consulConfig.ConsulAddress;

        var server = app.Services.GetRequiredService<IServer>();
        var addresses = server.Features.Get<IServerAddressesFeature>()?.Addresses;
        //var urls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS")?.Split(';')
        //               .FirstOrDefault(u => u.StartsWith("http://", StringComparison.OrdinalIgnoreCase));
        
        var httpAddress = addresses?.FirstOrDefault(a => a.StartsWith("http://", StringComparison.OrdinalIgnoreCase));
        
        if (httpAddress is not null)
        {
            var port = new Uri(httpAddress).Port;
            consulConfig.ServiceAddress = $"host.docker.internal:{port}";
        }
        else
        {
            Console.WriteLine("No server addresses found.");
        }
    }
    else
    {
        Console.WriteLine("Running with Docker");
    }

    var consulClient = new ConsulClient(x => x.Address = new Uri(consulConfig.ConsulAddress));
    var registration = new AgentServiceRegistration
    {
        ID = consulConfig.ServiceId,
        Name = consulConfig.ServiceName,
        Address = consulConfig.ServiceAddress,
        Port = consulConfig.ServicePort,
        Check = new AgentServiceCheck
        {
            HTTP = $"http://{consulConfig.ServiceAddress}{consulConfig.HealthCheckUrl}",
            Interval = TimeSpan.FromSeconds(10),
            Timeout = TimeSpan.FromSeconds(5),
            DeregisterCriticalServiceAfter = TimeSpan.FromMicroseconds(consulConfig.DeregisterAfterMinutes),
        }
    };


    // Register service with Consul
    consulClient.Agent.ServiceRegister(registration).Wait();
    Console.WriteLine($"AuthService Load Balancer with Nginx registred successfully");


});



app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}
app.UseRouting();
app.UseMetricServer();
app.UseHttpMetrics();

app.UseCors(corsPolicy);
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
