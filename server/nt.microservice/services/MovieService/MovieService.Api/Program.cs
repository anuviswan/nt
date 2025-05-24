using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MovieService.Api;
using MovieService.Api.Helpers;
using MovieService.Api.Settings;
using MovieService.Data;
using System;
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
ValueInjectorMapper.RegisterTypes();
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

app.UseHttpsRedirection();
app.UseCors(corsPolicy);
app.UseAuthorization();

app.MapControllers();
app.MapGraphQL();



app.Run();
