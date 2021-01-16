﻿using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Nt.Domain.Entities.Movie;
using MongoDB.Bson;
using MongoDB.Driver;
using Nt.Domain.RepositoryContracts.Movie;
using System.Threading.Tasks;

namespace Nt.Infrastructure.Data.Repositories.Movie
{
    public class ReviewRepository : GenericRepository<ReviewEntity>, IReviewRepository
    {
        public ReviewRepository(IMongoDatabase mongoDatabase):base(mongoDatabase)
        {

        }
        public async Task<IEnumerable<ReviewEntity>> FilterReviews(IEnumerable<string> userIdCollection)
        {
            var filter = Builders<ReviewEntity>.Filter.In(x => x.AuthorId, userIdCollection);
            return await _dataCollection.Find<ReviewEntity>(filter).ToListAsync();
        }
    }
}
