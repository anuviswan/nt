using MongoDB.Driver;
using ReviewService.Domain.Entities;
using ReviewService.Domain.Repositories;
using System.Data;

namespace ReviewService.Infrastructure.Repository.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity, new()
{
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<TEntity> _collection;
    public GenericRepository(IMongoDatabase database, string collectionNaem)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database));
        _collection = _database.GetCollection<TEntity>(collectionNaem ?? throw new ArgumentNullException(nameof(collectionNaem)));
    }

    public IMongoCollection<TEntity> Collection => _collection;
    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await _collection.Find(Builders<TEntity>.Filter.Empty).ToListAsync();

    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        var filter = Builders<TEntity>.Filter.Eq(e => e.Id, id);
        var entity = await _collection.Find(filter).FirstOrDefaultAsync();
        return entity ?? throw new KeyNotFoundException($"Entity with ID {id} not found.");
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _collection.InsertOneAsync(entity).ConfigureAwait(false);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var filter = Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id);
        await _collection.ReplaceOneAsync(filter,entity).ConfigureAwait(false);
        return entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        var filter = Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id);
        await _collection.DeleteOneAsync(filter).ConfigureAwait(false);
        return entity;
    }
}
