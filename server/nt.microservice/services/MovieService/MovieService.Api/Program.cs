using Consul;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MovieService.Api;
using MovieService.Api.BackgroundServices;
using MovieService.Api.Helpers;
using MovieService.Api.Settings;
using MovieService.Data;
using nt.shared.dto.Configurations;
using System.Text.Json.Serialization;

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

builder.Configuration
.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
.AddEnvironmentVariables(); // <- This ensures env vars are considered

builder.Services.Configure<ServiceRegistrationConfig>(builder.Configuration.GetSection(nameof(ServiceRegistrationConfig)));
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.Converters.Add(new MovieService.Api.JsonConverters.DateOnlyJsonConverter());
});


builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MovieDatabase"));
builder.Services.Configure<LoggerConfiguration>(builder.Configuration.GetSection(nameof(LoggerConfiguration)));

//builder.AddMongoDBClient("nt.movieservice.db");

//var mongoClient = builder.Services.BuildServiceProvider().GetRequiredService<IMongoClient>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterServices();
builder.Services.RegisterGraphQl();
ValueInjectorMapper.RegisterTypes();

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

var app = builder.Build();


app.MapDefaultEndpoints();



using (var scope = app.Services.CreateScope())
{
    var moduleInitializer = scope.ServiceProvider.GetService<ModuleInitializer>();
    if(moduleInitializer is not null)
    { 
        await moduleInitializer.Initialize(); 
    }
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors(corsPolicy);
app.UseAuthorization();

app.MapControllers();
app.MapGraphQL();



app.Run();

public class ExampleService(IMongoClient client)
{
    public void DoSomething()
    {
        // Example method to demonstrate service usage
        var database = client.GetDatabase("MovieServiceDb");
        // Perform operations with the database
    }
}