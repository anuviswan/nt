using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nt.WebApi.Repository.Repositories;
using Nt.WebApi.Shared.IRepositories;
using Nt.WebApi.Shared.Settings;

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
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddSingleton<IUserRepository>(x => new UserRepository(x.GetRequiredService<IUserDatabaseSettings>()));
            services.AddSingleton<IMovieRepository>(x => new MovieRepository(x.GetRequiredService<IMovieDatabaseSettings>()));
        }
        private void ConfigureDatabaseSettings(IServiceCollection services)
        {
            services.Configure<UserDatabaseSettings>(Configuration.GetSection(nameof(UserDatabaseSettings)));
            services.AddSingleton<IUserDatabaseSettings>(sp => sp.GetRequiredService<IOptions<UserDatabaseSettings>>().Value);

            services.Configure<MovieDatabaseSettings>(Configuration.GetSection(nameof(MovieDatabaseSettings)));
            services.AddSingleton<IMovieDatabaseSettings>(sp => sp.GetRequiredService<IOptions<MovieDatabaseSettings>>().Value);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }



            app.UseHttpsRedirection();
            app.UseMvcWithDefaultRoute();
        }
    }
}
