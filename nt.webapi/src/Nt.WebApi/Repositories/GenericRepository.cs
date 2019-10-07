using MongoDB.Driver;
using Nt.WebApi.Interfaces;
using Nt.WebApi.Interfaces.Repository;
using Nt.WebApi.Models;
using System;
using System.Collections.Generic;

namespace Nt.WebApi.Services
{
    public class GenericRepositoryService<TEntity,TDataContext>:IGenericRepository<TEntity,TDataContext> where TEntity : class, IBaseEntity
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
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
