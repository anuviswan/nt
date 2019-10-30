using MongoDB.Driver;
using Nt.WebApi.Shared.Entities;
using Nt.WebApi.Shared.Exceptions;
using Nt.WebApi.Shared.IRepositories;
using Nt.WebApi.Shared.Settings;
using System.Collections.Generic;
using System.Linq;

namespace Nt.WebApi.Repository.Repositories
{
    public class UserRepository : GenericRepositoryService<UserEntity, IUserDatabaseSettings>, IUserRepository
    {
        public UserRepository(IUserDatabaseSettings settings) : base(settings)
        {
        }

        public override IEnumerable<UserEntity> Get() => _dataCollection.Find(user => true).ToList();

        public UserEntity Get(string id) => _dataCollection.Find<UserEntity>(user => user.Id == id).FirstOrDefault();

        public bool CheckIfUserExists(string userName) => _dataCollection.Find<UserEntity>(user => user.UserName.Equals(userName)).Any();

        public UserEntity ValidateUser(string userName, string passKey)
        {
            var result = _dataCollection.Find<UserEntity>(user => user.UserName.Equals(userName) && user.PassKey.Equals(passKey));
            if (result.Any())
                return result.First();
            else
                throw new InvalidUserException();
        }

        public override UserEntity Create(UserEntity user)
        {
            return base.Create(user);
        }

        public void Update(string id, UserEntity userIn) => _dataCollection.ReplaceOne(user => user.Id == id, userIn);

        public void Remove(UserEntity userIn) =>
            _dataCollection.DeleteOne(book => book.Id == userIn.Id);

        public void Remove(string id) =>
            _dataCollection.DeleteOne(user => user.Id == id);
    }
}
