using Nt.WebApi.Shared.Entities;
using Nt.WebApi.Shared.Settings;
using System.Threading.Tasks;

namespace Nt.WebApi.Shared.IRepositories
{
    public interface IUserRepository : IGenericRepository<UserEntity, IUserDatabaseSettings>
    {
        Task<UserEntity> ValidateUserAsync(string userName, string passKey);
        Task<bool> CheckIfUserExistsAsync(string userName);
    }
}
