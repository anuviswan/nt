using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using UserService.Api.Controllers;
using MassTransit;
using UserService.Api.Settings;
using UserService.Api.ConsumerServices;
using Serilog.Formatting.Compact;
using Prometheus;
using UserService.Service.Services;

var builder = WebApplication.CreateBuilder(args);
var rabbitMqSettings = builder.Configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();
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
