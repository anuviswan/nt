using MovieService.Api;
using MovieService.Api.Helpers;
using MovieService.Api.Settings;
using MovieService.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.Converters.Add(new MovieService.Api.JsonConverters.DateOnlyJsonConverter());
});


builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MovieDatabase"));
builder.Services.Configure<LoggerConfiguration>(builder.Configuration.GetSection(nameof(LoggerConfiguration)));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterServices();
builder.Services.RegisterGraphQl();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGraphQL();

var serviceProvider = app.Services;
var moduleInitializer = serviceProvider.GetService<ModuleInitializer>();
moduleInitializer?.Initialize().Wait();

app.Run();
