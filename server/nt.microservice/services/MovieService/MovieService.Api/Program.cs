using MovieService.Api.Loggers;
using MovieService.Api.Settings;
using MovieService.Data;
using MovieService.Data.Interfaces.Services;
using MovieService.Data.Services;
using MovieService.Service.Interfaces.Services;

var builder = WebApplication.CreateBuilder(args);
var loggerConfiguration = builder.Configuration
                              .GetSection(nameof(LoggerConfiguration))
                              .Get<LoggerConfiguration>();

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("MovieDatabase"));

if (loggerConfiguration is null)
    throw new Exception("Logger not found !");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMovieService,MovieService.Service.Services.MovieService>();

builder.Services.AddSingleton<IMovieCrudService, MovieCrudService>();
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

app.Run();
