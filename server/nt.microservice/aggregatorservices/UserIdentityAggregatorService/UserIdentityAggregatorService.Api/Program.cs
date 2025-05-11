
using UserIdentityAggregatorService.Api.Services.AuthService;
using UserIdentityAggregatorService.Api.Services.UserService;
using UserIdentityAggregatorService.Api.Settings;
using Microsoft.Extensions.ServiceDiscovery.Dns;

namespace UserIdentityAggregatorService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var serviceDiscovery = builder.Configuration.GetSection(nameof(ServiceDiscovery)).Get<ServiceDiscovery>();
            builder.Services.AddServiceDiscovery();


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddHttpClient<AuthService>(options =>
            {
                options.BaseAddress = new Uri("http://host.docker.internal:8100/");
            });

            builder.Services.AddHttpClient<UserService>(options =>
            {
                options.BaseAddress = new Uri("http://host.docker.internal:8301/");
            });

            var app = builder.Build();

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
}
