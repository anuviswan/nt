﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nt.WebApi.Models;
using Nt.WebApi.Services;
using AutoMapper;
using Nt.WebApi.Interfaces.Services;

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
            services.AddDbContext<TodoContext>(opt =>
                 opt.UseInMemoryDatabase("TodoList"));

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
