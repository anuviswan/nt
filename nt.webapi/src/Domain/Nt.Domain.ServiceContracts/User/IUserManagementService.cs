using Nt.Domain.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nt.Domain.ServiceContracts.User
{
    public interface IUserManagementService
    {
        Task<IEnumerable<UserProfileEntity>> GetAllUsersAsync();
        Task<IEnumerable<UserProfileEntity>> SearchUserAsync(string userName=null);
        
        Task FollowUserRequestGetAsync(UserProfileEntity currentUser, UserProfileEntity userToFollow);
        Task ApproveFollowRequestAsync(UserProfileEntity currentUser, UserProfileEntity approveUserRequest);
    }
}
