using MongoDB.Driver;
using Nt.WebApi.Shared.Entities;
using Nt.WebApi.Shared.IRepositories;
using Nt.WebApi.Shared.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nt.WebApi.Repository.Repositories
{
    public class GenericRepositoryService<TEntity, TDataContext> : IGenericRepository<TEntity, TDataContext> where TEntity : class, IBaseEntity
                                                               where TDataContext : IDatabaseSettings
    {
        protected readonly IMongoCollection<TEntity> _dataCollection;

        public GenericRepositoryService(TDataContext settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _dataCollection = database.GetCollection<TEntity>(settings.CollectionName);
        }
        public virtual async Task<TEntity> CreateAsync(TEntity data)
        {
            await _dataCollection.InsertOneAsync(data);
            return data;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await Task.FromResult(_dataCollection.AsQueryable());
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Func<TEntity, bool> predicate)
        {
            return await Task.FromResult(_dataCollection.AsQueryable());
        }
    }
}
