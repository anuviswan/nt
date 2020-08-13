using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nt.Infrastructure.Data.Repositories.User;
using Nt.Domain.RepositoryContracts.User;
using Nt.Domain.RepositoryContracts.Movie;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using Nt.Domain.ServiceContracts.User;
using Nt.Application.Services.User;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.Entities.Settings;
using Nt.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Nt.Infrastructure.WebApi.Authentication;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Components;
using Nt.Domain.ServiceContracts.Movie;
using Nt.Application.Services.Movie;

namespace Nt.WebApi
{
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
            ConfigureDatabaseSettings(services);
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
                      
            ConfigureRepositories(services);
            ConfigureUnitOfWork(services);
            ConfigureAppServices(services);
            
            services.AddSwaggerGen();
        }

        private void ConfigureUnitOfWork(IServiceCollection services)
        {
            services.AddSingleton<IUnitOfWork>(x=>new UnitOfWork(x.GetRequiredService<IDatabaseSettings>()));
        }

        private void ConfigureAppServices(IServiceCollection services)
        {
            
            services.AddSingleton<IUserManagementService>(x => new UserManagementService(x.GetRequiredService<IUnitOfWork>()));
            services.AddSingleton<IUserProfileService>(x => new UserProfileService(x.GetRequiredService<IUnitOfWork>(),x.GetRequiredService<IUserManagementService>()));
            services.AddSingleton<IMovieService>(x=> new MovieService(x.GetRequiredService<IUnitOfWork>()));
            services.AddSingleton<IConfiguration>(x => Configuration);
            services.AddSingleton<ITokenGenerator>(x => new JwtTokenGenerator(x.GetRequiredService<IConfiguration>()));
        }
        private void ConfigureRepositories(IServiceCollection services)
        {
            //services.AddSingleton<IUserProfileRepository>(x => new UserProfileRepository(x.GetRequiredService<IUserDatabaseSettings>()));
            //services.AddSingleton<IMovieRepository>(x => new MovieRepository(x.GetRequiredService<IMovieDatabaseSettings>()));
        }
        private void ConfigureDatabaseSettings(IServiceCollection services)
        {
            services.Configure<NtDatabaseSettings>(Configuration.GetSection(nameof(NtDatabaseSettings)));
            services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<NtDatabaseSettings>>().Value);
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

}
