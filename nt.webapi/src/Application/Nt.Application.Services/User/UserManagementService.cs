using Nt.Domain.Entities.User;
using Nt.Domain.ServiceContracts.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nt.Application.Services.User
{
    public class UserManagementService : IUserManagementService
    {
        public Task ApproveFollowRequestAsync(UserProfileEntity currentUser, UserProfileEntity approveUserRequest)
        {
            throw new NotImplementedException();
        }

        public Task FollowUserRequestGetAsync(UserProfileEntity currentUser, UserProfileEntity userToFollow)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfileEntity> GetUserAsync(UserProfileEntity user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserProfileEntity>> SearchUserAsync(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
