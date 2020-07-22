using MongoDB.Driver;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Nt.Infrastructure.Data.Repositories.User
{
    public class UserProfileRepository : GenericRepository<UserProfileEntity>, IUserProfileRepository
    {
        public UserProfileRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {

        }
        public async Task<UserProfileEntity> ValidateUserAsync(string userName, string passKey)
        {
            var result = await _dataCollection.FindAsync(user => user.UserName.Equals(userName) && user.PassKey.Equals(passKey));
            if (result.Any())
                return result.First();
            else
                throw new InvalidUserException();
        }
    }
}
