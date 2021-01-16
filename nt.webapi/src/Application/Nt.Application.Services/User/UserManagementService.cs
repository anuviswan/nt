using JExtensions.Linq;
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

        public async Task FollowUserAsync(string currentUserName, string userNameToFollow)
        {
            var currentUserEntity = await GetUserAsync(currentUserName);
            if (currentUserEntity == null) 
                throw new EntityNotFoundException();

            var userEntityToFollow = await GetUserAsync(userNameToFollow);
            var followers = userEntityToFollow.Followers?.ToList()?? Enumerable.Empty<string>().ToList();
            followers.Add(currentUserName);

            var updatedUserToFollow = userEntityToFollow with { Followers = followers };
            await UnitOfWork.UserProfileRepository.UpdateAsync(updatedUserToFollow);


            var currentUser = await GetUserAsync(currentUserName);
            var follows = currentUser.Follows?.ToList() ?? Enumerable.Empty<string>().ToList();
            follows.Add(userNameToFollow);
            var updatedCurrentUser = currentUser with { Follows = follows };
            await UnitOfWork.UserProfileRepository.UpdateAsync(updatedCurrentUser);
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

            return userSearchResult.Where(x=>x.UserName.Equals(userName,StringComparison.InvariantCultureIgnoreCase)) switch
            {
                IEnumerable<UserProfileEntity> users when !users.Any() => throw new EntityNotFoundException(),
                IEnumerable<UserProfileEntity> users when  users.Count() == 1 => users.Single(),
                _ => throw new Exception("Multiple Users Found.")
            };
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

        public Task UnfollowUserAsync(UserProfileEntity currentUser, UserProfileEntity userToUnfollow)
        {
            throw new NotImplementedException();
        }
    }
}
