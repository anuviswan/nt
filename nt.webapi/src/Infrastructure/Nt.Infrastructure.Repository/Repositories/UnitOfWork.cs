using MongoDB.Driver;
using Nt.Domain.Entities.Movie;
using Nt.Domain.Entities.Settings;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Infrastructure.Data.Repositories.Movie;
using System;

namespace Nt.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly MongoClient _mongoClient;
        protected readonly IMongoDatabase _mongoDatabase;
        private Lazy<GenericRepository<UserProfileEntity>> _userProfileRepository;
        private Lazy<GenericRepository<MovieEntity>> _movieRepository;
        private Lazy<GenericRepository<ReviewEntity>> _reviewRepository;

        public UnitOfWork(IDatabaseSettings settings)
        {
            _mongoClient = new MongoClient(settings.ConnectionString);
            _mongoDatabase = _mongoClient.GetDatabase(settings.DatabaseName);

            _userProfileRepository = new Lazy<GenericRepository<UserProfileEntity>>(() => new GenericRepository<UserProfileEntity>(_mongoDatabase));
            _movieRepository = new Lazy<GenericRepository<MovieEntity>>(() => new GenericRepository<MovieEntity>(_mongoDatabase));
            _reviewRepository = new Lazy<GenericRepository<ReviewEntity>>(() => new GenericRepository<ReviewEntity>(_mongoDatabase));
        }


        public IGenericRepository<UserProfileEntity> UserProfileRepository => _userProfileRepository.Value;
        public IGenericRepository<MovieEntity> MovieRepository => _movieRepository.Value;
        public IGenericRepository<ReviewEntity> ReviewRepository => _reviewRepository.Value;

    }
}
