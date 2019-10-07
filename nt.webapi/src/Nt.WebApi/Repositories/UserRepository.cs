using MongoDB.Driver;
using Nt.WebApi.Exceptions;
using Nt.WebApi.Interfaces.Services;
using Nt.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.WebApi.Services
{
    public class UserRepository : GenericRepositoryService<UserEntity,IUserDatabaseSettings>, IUserRepository
    {
        public UserRepository(IUserDatabaseSettings settings):base(settings)
        {
        }

        public override IEnumerable<UserEntity> Get() => _dataCollection.Find(user => true).ToList();

        public UserEntity Get(string id) => _dataCollection.Find<UserEntity>(user => user.Id == id).FirstOrDefault();

        public bool CheckIfUserExists(string userName) => _dataCollection.Find<UserEntity>(user => user.UserName.Equals(userName)).Any();

        public UserEntity ValidateUser(string userName,string passKey)
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
