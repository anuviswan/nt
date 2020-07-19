using MongoDB.Driver;
using Nt.Domain.Entities.Settings;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using System;

namespace Nt.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly MongoClient _mongoClient;
        protected readonly IMongoDatabase _mongoDatabase;
        private Lazy<GenericRepository<UserProfileEntity>> _userProfileRepository;

        public UnitOfWork(IDatabaseSettings settings)
        {
            _mongoClient = new MongoClient(settings.ConnectionString);
            _mongoDatabase = _mongoClient.GetDatabase(settings.DatabaseName);

            _userProfileRepository = new Lazy<GenericRepository<UserProfileEntity>>(() => new GenericRepository<UserProfileEntity>(_mongoDatabase));
        }


        public IGenericRepository<UserProfileEntity> UserProfileRepository => _userProfileRepository.Value;

    }
}
