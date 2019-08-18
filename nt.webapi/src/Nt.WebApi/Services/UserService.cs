﻿using MongoDB.Driver;
using Nt.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.WebApi.Services
{
    public class UserService
    {
        private readonly IMongoCollection<UserDto> _users;

        public UserService(IUserDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<UserDto>(settings.CollectionName);
        }

        public List<UserDto> Get() => _users.Find(user => true).ToList();

        public UserDto Get(string id) => _users.Find<UserDto>(user => user.Id == id).FirstOrDefault();

        public UserDto Create(UserDto user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, UserDto userIn) => _users.ReplaceOne(book => book.Id == id, userIn);

        public void Remove(UserDto userIn) =>
            _users.DeleteOne(book => book.Id == userIn.Id);

        public void Remove(string id) =>
            _users.DeleteOne(user => user.Id == id);
    }
}
