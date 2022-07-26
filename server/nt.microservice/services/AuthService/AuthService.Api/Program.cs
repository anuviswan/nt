using AuthService.Api.Authentication;
using AuthService.Api.Helpers.ExtensionMethods;
using AuthService.Api.ViewModels.Validatators;
using AuthService.Api.ViewModels.Validate;
using AuthService.Data.Database;
using AuthService.Data.Repository;
using AuthService.Service.Query;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;
using System.Text;
using NLog;
using NLog.Web;
using MassTransit;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDapperTypeMaps();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var config = TypeAdapterConfig.GlobalSettings;
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

builder.Services.AddSingleton<ITokenGenerator,JwtTokenGenerator>();
builder.Services.AddMediatR(typeof(ValidateUserQuery).Assembly);
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var connectionString = builder.Configuration.GetConnectionString("UserSqlDb");
builder.Services.AddTransient<IUnitOfWorkFactory>(con => new PgUnitOfWorkFactory(connectionString));

builder.Services.AddFluentValidation();
builder.Services.AddValidators();

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

builder.Services.AddLogging(c =>
{
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudiences = new string[]
            {
                builder.Configuration["Jwt:Aud1"],
                builder.Configuration["Jwt:Aud2"]
            },
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
