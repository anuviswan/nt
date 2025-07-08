
using Couchbase;
using Microsoft.Extensions.Options;
using ReviewService.Presenation.Api.HostedServices;
using ReviewService.Presenation.Api.Options;

namespace ReviewService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        
        builder.Services.Configure<CouchbaseSettings>(builder.Configuration.GetSection(nameof(CouchbaseSettings)));

        builder.Services.AddSingleton<ICluster>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<CouchbaseSettings>>().Value;
            var cluster = Cluster.ConnectAsync(options.ConnectionString, options.Username, options.Password)
                                 .GetAwaiter().GetResult();
            return cluster;
        });
        builder.Services.AddHostedService<EnsureBucketService>();

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        app.MapDefaultEndpoints();

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




