using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.ServiceContracts.User;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Nt.Application.Services.User
{
    public class UserProfileService : ServiceBase, IUserProfileService
    {
        private IUserManagementService _userManagementService;
        public UserProfileService(IUnitOfWork unitOfWork,IUserManagementService userManagementService) : base(unitOfWork)
        {
            _userManagementService = userManagementService;
        }

        public async Task<UserProfileEntity> AuthenticateAsync(UserProfileEntity userProfile)
        {
            var existingUser = await UnitOfWork.UserProfileRepository.GetAsync(x => x.UserName.ToLower() == userProfile.UserName.ToLower() 
                                                                                    && x.PassKey == userProfile.PassKey
                                                                                    && !x.IsDeleted);
            if (!existingUser.Any())
            {
                throw new InvalidPasswordOrUsernameException();
            }
            return existingUser.Single();
        }

        public Task<UserProfileEntity> ChangePasswordAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<UserProfileEntity> CreateUserAsync(UserProfileEntity userProfile)
        {
            var existingUser = await UnitOfWork.UserProfileRepository.GetAsync(x => x.UserName.ToLower() == userProfile.UserName.ToLower());
            if (existingUser.Any())
            {
                throw new UserNameExistsException();
            }
            return await UnitOfWork.UserProfileRepository.CreateAsync(userProfile);
        }


        public Task<UserProfileEntity> GetUserAsync(UserProfileEntity user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateUserAsync(UserProfileEntity user)
        {
            var userFound = await _userManagementService.SearchUserAsync(user.UserName);
            if(!userFound.Any())
            {
                throw new EntityNotFoundException();
            }
            var userToChange = userFound.Single();

            userToChange.Bio = user.Bio;
            userToChange.DisplayName = user.DisplayName;

            return await UnitOfWork.UserProfileRepository.UpdateAsync(userToChange);
        }
    }
}
