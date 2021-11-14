﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nt.Infrastructure.WebApi.Bootstrap;
using System.Text;

namespace Nt.WebApi;
public class Startup
{
    readonly string CorsPolicy = "_ntClientAppsOrigins";
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
        services.AddAutoMapper(typeof(Startup));
        ServiceConfiguration.ConfigureDatabaseSettings(services,Configuration);
        services.AddCors(option=> {
            option.AddPolicy(name: CorsPolicy,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
        });

        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        services.AddSingleton<IConfiguration>(x => Configuration);
        ServiceConfiguration.ConfigureRepositories(services);
        ServiceConfiguration.ConfigureUnitOfWork(services);
        ServiceConfiguration.ConfigureAppServices(services);
            
        services.AddSwaggerGen();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors(CorsPolicy);
        // Enable middleware to serve generated Swagger as a JSON endpoint.
        app.UseSwagger();

        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
        // specifying the Swagger JSON endpoint.
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        });

        if (env.IsDevelopment())
        { 
            app.UseDeveloperExceptionPage(); 
        }

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
            
    }
}
