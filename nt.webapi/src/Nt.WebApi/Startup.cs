using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nt.WebApi.Interfaces.Services;
using Nt.WebApi.Repository.Repositories;
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
            services.Configure<UserDatabaseSettings>(Configuration.GetSection(nameof(UserDatabaseSettings)));


            services.AddSingleton<IUserDatabaseSettings>(sp => sp.GetRequiredService<IOptions<UserDatabaseSettings>>().Value);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<IUserRepository>(x=>new UserRepository(x.GetRequiredService<IUserDatabaseSettings>()));
            

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
