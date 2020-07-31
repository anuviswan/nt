using Nt.Domain.Entities.User;
using System.Threading.Tasks;

namespace Nt.Domain.ServiceContracts.User
{
    public interface IUserProfileService
    {
        Task<UserProfileEntity> GetUserAsync(UserProfileEntity user);
        Task<UserProfileEntity> AuthenticateAsync(UserProfileEntity userProfile);
        Task<UserProfileEntity> CreateUserAsync(UserProfileEntity userProfile);
        Task<UserProfileEntity> ChangePasswordAsync();
        Task<UserProfileEntity> UpdateUserAsync(UserProfileEntity userProfile);
    }
}
