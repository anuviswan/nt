using Microsoft.Extensions.Options;
using ReviewService.Presenation.Api;
using ReviewService.Presenation.Api.Helpers;
using ReviewService.Presenation.Api.Options;
using StackExchange.Redis;

namespace ReviewService.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();
        builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(nameof(DatabaseOptions)));
        builder.Services.Configure<CacheOptions>(builder.Configuration.GetSection(nameof(CacheOptions)));

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.RegisterServices();
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
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}


