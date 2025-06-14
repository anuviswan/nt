using Consul;
using Grpc.Net.Client.Configuration;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using nt.shared.dto.Configurations;
using Prometheus;
using Serilog;
using Serilog.Formatting.Compact;
using System.Text;
using UserService.Api.BackgroundServices;
using UserService.Api.ConsumerServices;
using UserService.Api.Controllers;
using UserService.Api.Settings;
using UserService.Service.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
.AddEnvironmentVariables(); // <- This ensures env vars are considered

builder.AddServiceDefaults();
var rabbitMqSettings = builder.Configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();
builder.Services.Configure<ServiceDiscoveryConfiguration>(builder.Configuration.GetSection(nameof(ServiceDiscoveryConfiguration)));

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
var connectionString = builder.Configuration.GetConnectionString("nt-userservice-db");

if(connectionString is null)
{
    throw new ArgumentNullException("Connection string 'nt-userservice-db' is not configured in appsettings.json or environment variables.");
}
builder.Services.AddDbContext<UserserviceDbContext>(o=>o.UseSqlServer(connectionString!));


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

builder.Services.AddSingleton<IConsulClient,ConsulClient>(sp =>
{
    var config = sp.GetRequiredService<IOptions<ServiceDiscoveryConfiguration>>().Value;
    var consulConfig = new ConsulClientConfiguration
    {
        Address = new Uri(config.ServiceDiscoveryAddress)
    };
    return new ConsulClient(consulConfig);
});

builder.Services.AddHostedService<ServiceRegistration>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<UserserviceDbContext>();
    // dbContext.Database.EnsureCreated(); // Ensure database is created (for code-first)
    var pendingMigrations = dbContext.Database.GetPendingMigrations();
    if (pendingMigrations.Any())
    {
        dbContext.Database.Migrate();
    }
}


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
