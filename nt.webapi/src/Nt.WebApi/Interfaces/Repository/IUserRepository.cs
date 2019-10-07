using Nt.WebApi.Interfaces.Repository;
using Nt.WebApi.Models;

namespace Nt.WebApi.Interfaces.Services
{
    public interface IUserRepository:IGenericRepository<UserEntity, IUserDatabaseSettings>
    {
        UserEntity ValidateUser(string userName, string passKey);
        bool CheckIfUserExists(string userName);
    }
}
