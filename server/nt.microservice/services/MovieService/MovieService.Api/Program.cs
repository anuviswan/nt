using MovieService.Api;
using MovieService.Api.Loggers;
using MovieService.Api.Settings;
using MovieService.Data;
using MovieService.Data.Interfaces.Services;
using MovieService.Data.Services;
using MovieService.Service.Interfaces.Services;
using System.ComponentModel;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var loggerConfiguration = builder.Configuration
                              .GetSection(nameof(LoggerConfiguration))
                              .Get<LoggerConfiguration>();

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("MovieDatabase"));


if (loggerConfiguration is null)
    throw new Exception("Logger not found !");
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.Converters.Add(new MovieService.Api.JsonConverters.DateOnlyJsonConverter());
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMovieService,MovieService.Service.Services.MovieService>();
builder.Services.AddSingleton<IMovieCrudService, MovieCrudService>();
builder.Services.AddSingleton<DatabaseInitializer>();
builder.Services.AddSingleton<ModuleInitializer>();


builder.Logging.AddProvider(new CustomLoggerProvider(loggerConfiguration!));

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


var serviceProvider = app.Services;
try
{
    var moduleInitializer = serviceProvider.GetService<ModuleInitializer>();
    moduleInitializer?.Initialize().Wait();
}
catch (Exception ex)
{
    var _ = ex;
    throw;
}

app.Run();
