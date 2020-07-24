using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Nt.Domain.Entities.Attributes;
using Nt.Domain.Entities.Entities;
using Nt.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Nt.Infrastructure.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IBaseEntity
    {
        protected readonly IMongoCollection<TEntity> _dataCollection;
        protected readonly MongoClient _mongoClient;
        protected readonly IMongoDatabase _mongoDatabase;

        public GenericRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
            _dataCollection = _mongoDatabase.GetCollection<TEntity>(GetCollectionName<TEntity>());
        }

        private string GetCollectionName<T>()
        {
            var type = typeof(T);
            return type.GetCustomAttribute<BsonCollectionAttribute>().CollectionName;
        }
        public virtual async Task<TEntity> CreateAsync(TEntity data)
        {
            await _dataCollection.InsertOneAsync(data);
            return data;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await _dataCollection.Find(_ => true).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dataCollection.AsQueryable<TEntity>().Where(predicate).ToListAsync();
        }
    }
}
