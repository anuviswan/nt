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
        public UserProfileService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }

        public Task<UserProfileEntity> AuthenticateAsync(string userName, string base64Key)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfileEntity> ChangePasswordAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<UserProfileEntity> CreateUserAsync(UserProfileEntity userProfile)
        {
            var existingUser = await UnitOfWork.UserProfileRepository.GetAsync(x => x.UserName.ToLower().Equals(userProfile.UserName.ToLower()));
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

        public Task<UserProfileEntity> UpdateUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}
