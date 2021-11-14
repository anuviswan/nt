using Microsoft.Extensions.Options;
using Nt.Application.Services.Movie;
using Nt.Application.Services.User;
using Nt.Domain.Entities.Settings;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.ServiceContracts.Movie;
using Nt.Domain.ServiceContracts.User;
using Nt.Infrastructure.Data.Repositories;
using Nt.Infrastructure.WebApi.Authentication;

namespace Nt.Infrastructure.WebApi.Bootstrap;
internal static class ServiceConfiguration
{
    internal static void ConfigureAppServices(IServiceCollection services)
    {
        services.AddSingleton<IUserManagementService>(x => new UserManagementService(x.GetRequiredService<IUnitOfWork>()));
        services.AddSingleton<IUserProfileService>(x => new UserProfileService(x.GetRequiredService<IUnitOfWork>(), x.GetRequiredService<IUserManagementService>()));
        services.AddSingleton<IMovieService>(x => new MovieService(x.GetRequiredService<IUnitOfWork>()));
        services.AddSingleton<IReviewService>(x => new ReviewService(x.GetRequiredService<IUnitOfWork>()));
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
