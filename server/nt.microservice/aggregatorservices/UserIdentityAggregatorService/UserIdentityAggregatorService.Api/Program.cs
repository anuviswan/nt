
using Consul;
using Microsoft.Extensions.Options;
using nt.shared.dto.Configurations;
using UserIdentityAggregatorService.Api.BackgroundServices;
using UserIdentityAggregatorService.Api.Services;
using UserIdentityAggregatorService.Api.Settings;

namespace UserIdentityAggregatorService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables(); // <- This ensures env vars are considered

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
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.Configure<ServiceMappingConfig>(builder.Configuration.GetSection(nameof(ServiceMappingConfig)));
        builder.Services.Configure<ServiceRegistrationConfig>(builder.Configuration.GetSection(nameof(ServiceRegistrationConfig)));

        builder.Services.AddSingleton<IConsulClient, ConsulClient>(sp =>
        {
            var config = sp.GetRequiredService<IOptions<ServiceRegistrationConfig>>().Value;
            var consulConfig = new ConsulClientConfiguration
            {
                Address = new Uri(config.RegistryUri)
            };
            return new ConsulClient(consulConfig);
        });
        builder.Services.AddHostedService<ServiceRegistration>();

        builder.Services.AddHttpClient();
        builder.Services.AddScoped<ConsulServiceResolver>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        //builder.Services.AddSingleton<IHttpClientFactory>(sp => new DevelopmentHttpClientFactory(sp));
        var app = builder.Build();
        //builder.Services.AddSingleton<IHttpClientFactory>(sp => new DevelopmentHttpClientFactory(sp));
        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
        app.UseCors(corsPolicy);
        //app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
