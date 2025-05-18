
using UserIdentityAggregatorService.Api.Services;
using UserIdentityAggregatorService.Api.Settings;

namespace UserIdentityAggregatorService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
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

        builder.Services.Configure<ServiceDiscoveryOptions>(builder.Configuration.GetSection(nameof(ServiceDiscoveryOptions)));
        builder.Services.AddHttpClient();
        builder.Services.AddScoped<ConsulServiceResolver>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        //builder.Services.AddSingleton<IHttpClientFactory>(sp => new DevelopmentHttpClientFactory(sp));
        var app = builder.Build();

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
