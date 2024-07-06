using MovieService.Api.Loggers;
using MovieService.Api.Settings;

var builder = WebApplication.CreateBuilder(args);
var loggerConfiguration = builder.Configuration
                              .GetSection(nameof(LoggerConfiguration))
                              .Get<LoggerConfiguration>();

if (loggerConfiguration is null)
    throw new Exception("Logger not found !");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
