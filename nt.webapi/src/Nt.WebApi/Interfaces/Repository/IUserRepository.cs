using Nt.WebApi.Interfaces.Repository;
using Nt.WebApi.Models;
using Nt.WebApi.Models.Settings;

namespace Nt.WebApi.Interfaces.Services
{
    public interface IUserRepository:IGenericRepository<UserEntity, IUserDatabaseSettings>
    {
        UserEntity ValidateUser(string userName, string passKey);
        bool CheckIfUserExists(string userName);
    }
}
