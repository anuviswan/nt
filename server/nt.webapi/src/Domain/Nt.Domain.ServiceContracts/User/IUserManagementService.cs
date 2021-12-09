using Nt.Domain.Entities.User;

namespace Nt.Domain.ServiceContracts.User;
public interface IUserManagementService
{
    Task<IEnumerable<UserProfileEntity>> GetAllUsersAsync();
    Task<IEnumerable<UserProfileEntity>> SearchUserAsync(string userName=null);
    Task<UserProfileEntity> GetUserAsync(string userName);
    Task FollowUserAsync(string currentUserName, string userNameToFollow);
    Task UnfollowUserAsync(string currentUserName, string userNameToFollow);
}
