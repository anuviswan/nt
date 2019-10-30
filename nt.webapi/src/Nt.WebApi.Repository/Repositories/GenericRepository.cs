using MongoDB.Driver;
using Nt.WebApi.Shared.Entities;
using Nt.WebApi.Shared.IRepositories;
using Nt.WebApi.Shared.Settings;
using System;
using System.Collections.Generic;

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
        public virtual TEntity Create(TEntity data)
        {
            _dataCollection.InsertOne(data);
            return data;
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return _dataCollection.AsQueryable();
        }

        public virtual IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
