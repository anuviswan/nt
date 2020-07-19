using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.ServiceContracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Application.Services.User
{
    public class UserManagementService : ServiceBase, IUserManagementService
    {
        public UserManagementService(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }
        public Task ApproveFollowRequestAsync(UserProfileEntity currentUser, UserProfileEntity approveUserRequest)
        {
            throw new NotImplementedException();
        }

        public Task FollowUserRequestGetAsync(UserProfileEntity currentUser, UserProfileEntity userToFollow)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserProfileEntity>> GetAllUsersAsync()
        {
            return await UnitOfWork.UserProfileRepository.GetAsync();
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
