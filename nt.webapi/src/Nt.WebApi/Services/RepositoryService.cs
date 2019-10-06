using MongoDB.Driver;
using Nt.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.WebApi.Services
{
    public class RepositoryService<T> where T :BaseDto
    {
        private readonly IMongoCollection<T> _dataCollection;

        public RepositoryService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _dataCollection = database.GetCollection<T>(settings.CollectionName);
        }
        public T Create(T dto)
        {
            _dataCollection.InsertOne(dto);
            return dto;
        }

    }
}
