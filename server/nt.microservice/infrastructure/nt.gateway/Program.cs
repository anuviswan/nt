using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using nt.gateway.Settings;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using Prometheus;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
var corsPolicy = "_ntClientAppsOrigins";

//Add services to the container.
builder.Services.AddCors(option => {
    option.AddPolicy(name: corsPolicy,
        builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        });
});


builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

builder.Services.AddLogging(c =>
{
    c.AddConsole();
});

builder.Services.AddRazorPages();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("GatewayAuthenticationKey", option =>
     {
         var jwt = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
         if ((jwt?.Validate()) != true)
         {
             throw new Exception("Unable to read Jwt Settings");
         }
         option.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = false,
             ValidateAudience = true,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             ValidIssuer = jwt.Issuer,
             ValidAudience = jwt.Aud1,
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
         };
     });

builder.Services.AddAuthentication(
        CertificateAuthenticationDefaults.AuthenticationScheme)
    .AddCertificate();

builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddOcelot(builder.Configuration)
       .AddConsul()
       .AddPolly()
       //.AddPollyWithInternalServerErrorHandling()
       .AddDelegatingHandler<LoggingHandler>(true).AddDelegatingHandler<DynamicHostReplacementHandler>(true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerForOcelot(builder.Configuration, (o)=> o.GenerateDocsForGatewayItSelf = true);
var app = builder.Build();

app.MapDefaultEndpoints();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}

app.Use(async (context, next) =>
{
    Console.WriteLine($"Incoming request: {context.Request.Method} {context.Request.Path}");
    await next();
});


app.UseCors(corsPolicy);
//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseMetricServer();
app.UseHttpMetrics();

//app.UseAuthentication();
//app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    //app.UseSwaggerForOcelotUI(opt =>
    //{
    //    opt.PathToSwaggerGenerator = "/swagger/docs";
    //}).UseOcelot().Wait();

    try
    {
        app.UseOcelot().Wait();
        app.Logger.LogInformation("Ocelot loaded successfully");
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "Failed to load Ocelot configuration");
    }

    app.UseSwagger();
}

app.MapRazorPages();

app.Run();
