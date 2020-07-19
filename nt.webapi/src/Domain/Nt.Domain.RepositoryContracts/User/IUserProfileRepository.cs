using Nt.Domain.Entities.User;
using System.Threading.Tasks;

namespace Nt.Domain.RepositoryContracts.User
{
    public interface IUserProfileRepository
    {

        Task<UserProfileEntity> ValidateUserAsync(string userName, string passKey);
        Task<bool> CheckIfUserExistsAsync(string userName);
    }
}
