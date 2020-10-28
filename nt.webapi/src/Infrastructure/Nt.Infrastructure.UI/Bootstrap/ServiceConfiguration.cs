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

namespace Nt.Infrastructure.WebApi.Bootstrap
{
    internal static class ServiceConfiguration
    {
        internal static void ConfigureAppServices(IServiceCollection services)
        {
            services.AddSingleton<IUserManagementService>(x => new UserManagementService(x.GetRequiredService<IUnitOfWork>()));
            services.AddSingleton<IUserProfileService>(x => new UserProfileService(x.GetRequiredService<IUnitOfWork>(), x.GetRequiredService<IUserManagementService>()));
            services.AddSingleton<IMovieService>(x => new MovieService(x.GetRequiredService<IUnitOfWork>()));
           
            services.AddSingleton<ITokenGenerator>(x => new JwtTokenGenerator(x.GetRequiredService<IConfiguration>()));
        }

        internal static void ConfigureUnitOfWork(IServiceCollection services)
        {
            services.AddSingleton<IUnitOfWork>(x => new UnitOfWork(x.GetRequiredService<IDatabaseSettings>()));
        }

        internal static void ConfigureDatabaseSettings(IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<NtDatabaseSettings>(configuration.GetSection(nameof(NtDatabaseSettings)));
            services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<NtDatabaseSettings>>().Value);
        }
        internal static void ConfigureRepositories(IServiceCollection services)
        {   
            //services.AddSingleton<IUserProfileRepository>(x => new UserProfileRepository(x.GetRequiredService<IUserDatabaseSettings>()));
            //services.AddSingleton<IMovieRepository>(x => new MovieRepository(x.GetRequiredService<IMovieDatabaseSettings>()));
        }
    }
}
