
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

        //builder.Services.AddSingleton<ICluster>(sp =>
        //{
        //    var options = sp.GetRequiredService<IOptions<CouchbaseSettings>>().Value;
        //    var cluster = Cluster.ConnectAsync(options.ConnectionString, options.Username, options.Password)
        //                         .GetAwaiter().GetResult();
        //    Cluster.ConnectAsync()
        //    return cluster;
        //});
        builder.Services.AddSingleton<IClusterProvider, LazyClusterProvider>();
        builder.Services.AddHostedService<ClusterBootstrapService>();
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




public interface IClusterProvider
{
    Task<ICluster> GetClusterAsync();
}

public class LazyClusterProvider : IClusterProvider
{
    private readonly CouchbaseSettings _settings;
    private ICluster _cluster;

    public LazyClusterProvider(IOptions<CouchbaseSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task<ICluster> GetClusterAsync()
    {
        if (_cluster != null) return _cluster;

        _cluster = await Cluster.ConnectAsync(
            _settings.ConnectionString,
            _settings.Username,
            _settings.Password);

        return _cluster;
    }
}
