using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.ServiceContracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return await UnitOfWork.UserProfileRepository.GetAsync().ConfigureAwait(false);
        }

        public async Task<UserProfileEntity> GetUserAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new EntityNotFoundException();
            }

            var userSearchResult = await SearchUserAsync(userName).ConfigureAwait(false);
            if (userSearchResult.Count() == 1 && userSearchResult.Single().UserName.ToLower() == userName.ToLower())
                return userSearchResult.First();

            throw new Exception("Multiple user found");
        }


        public async Task<IEnumerable<UserProfileEntity>> SearchUserAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return await Task.FromResult(Enumerable.Empty<UserProfileEntity>());
            }
            return await UnitOfWork.UserProfileRepository.GetAsync(x => (x.DisplayName.ToLower().StartsWith(userName.ToLower()) 
                                                                        || x.UserName.ToLower().StartsWith(userName.ToLower())) 
                                                                        && !x.IsDeleted);
        }
    }
}
