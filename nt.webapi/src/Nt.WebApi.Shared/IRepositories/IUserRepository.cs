using Nt.WebApi.Shared.Entities;
using Nt.WebApi.Shared.IRepositories;
using Nt.WebApi.Shared.Settings;

namespace Nt.WebApi.Interfaces.Services
{
    public interface IUserRepository : IGenericRepository<UserEntity, IUserDatabaseSettings>
    {
        UserEntity ValidateUser(string userName, string passKey);
        bool CheckIfUserExists(string userName);
    }
}
