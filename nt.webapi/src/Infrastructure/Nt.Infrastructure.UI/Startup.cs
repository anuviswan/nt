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

namespace Nt.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            ConfigureDatabaseSettings(services);
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
            services.AddSingleton<IUserProfileService>(x => new UserProfileService(x.GetRequiredService<IUnitOfWork>()));
            services.AddSingleton<IUserManagementService>(x => new UserManagementService(x.GetRequiredService<IUnitOfWork>()));
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

            services.Configure<UserDatabaseSettings>(Configuration.GetSection(nameof(UserDatabaseSettings)));
            services.AddSingleton<IUserDatabaseSettings>(sp => sp.GetRequiredService<IOptions<UserDatabaseSettings>>().Value);

            services.Configure<MovieDatabaseSettings>(Configuration.GetSection(nameof(MovieDatabaseSettings)));
            services.AddSingleton<IMovieDatabaseSettings>(sp => sp.GetRequiredService<IOptions<MovieDatabaseSettings>>().Value);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
