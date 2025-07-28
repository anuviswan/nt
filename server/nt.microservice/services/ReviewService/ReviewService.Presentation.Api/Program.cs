using Consul;
using Microsoft.Extensions.Options;
using nt.shared.dto.Configurations;
using ReviewService.Presenation.Api;
using ReviewService.Presenation.Api.BackgroundServices;
using ReviewService.Presenation.Api.Helpers;
using ReviewService.Presenation.Api.Options;

namespace ReviewService.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

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


        builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(nameof(DatabaseOptions)));
        builder.Services.Configure<CacheOptions>(builder.Configuration.GetSection(nameof(CacheOptions)));
        builder.Services.Configure<ServiceRegistrationConfig>(builder.Configuration.GetSection(nameof(ServiceRegistrationConfig)));

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.RegisterServices();
        builder.Services.AddEndpointsApiExplorer(); // for minimal APIs
        builder.Services.AddSwaggerGen();           // generates the Swagger JSON

        builder.Services.AddSingleton<IConsulClient, ConsulClient>(sp =>
        {
            var config = sp.GetRequiredService<IOptions<ServiceRegistrationConfig>>().Value;
            var consulConfig = new ConsulClientConfiguration
            {
                Address = new Uri(config.RegistryUri)
            };
            return new ConsulClient(consulConfig);
        });

        builder.Services.AddHostedService<ConsulServiceRegistrationService>();

        var app = builder.Build();

        app.MapDefaultEndpoints();


        using (var scope = app.Services.CreateScope())
        {
            var moduleInitializer = scope.ServiceProvider.GetService<ModuleInitializer>();
            if (moduleInitializer is not null)
            {
                await moduleInitializer.Initialize();
            }
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();               // serves /swagger/v1/swagger.json
            app.UseSwaggerUI();             // serves /swagger
        }

       // app.UseHttpsRedirection();
        app.UseCors(corsPolicy);
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}


