using Nt.Domain.Entities.User;
using System.Threading.Tasks;

namespace Nt.Domain.ServiceContracts.User
{
    public interface IUserProfileService
    {
        Task<UserProfileEntity> AuthenticateAsync(string userName,string base64Key);
        Task<UserProfileEntity> CreateUserAsync();
        Task<UserProfileEntity> ChangePasswordAsync();
        Task<UserProfileEntity> UpdateUserAsync();
    }
}
