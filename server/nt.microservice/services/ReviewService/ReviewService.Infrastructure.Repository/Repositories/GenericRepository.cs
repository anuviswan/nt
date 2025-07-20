using AutoMapper;
using MongoDB.Driver;
using MongoDB.Entities;
using ReviewService.Domain.Repositories;

namespace ReviewService.Infrastructure.Repository.Repositories;

public class GenericRepository<TDocument,TDomain>(IMongoDatabase database, IMapper mapper, string collectionName) : IGenericRepository<TDomain> 
    where TDomain : class, Domain.Entities.IEntity, new()
    where TDocument : Entity
{
    protected readonly IMapper Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    protected IMongoDatabase Database => database ?? throw new ArgumentNullException(nameof(database));
    protected IMongoCollection<TDocument> Collection => database.GetCollection<TDocument>(collectionName ?? throw new ArgumentNullException(nameof(collectionName)));
    public async Task<IEnumerable<TDomain>> GetAll()
    {
        var result = await Collection.Find(Builders<TDocument>.Filter.Empty).ToCursorAsync().ConfigureAwait(false);
        return  Mapper.Map<IEnumerable<TDomain>>(result.ToEnumerable()) ?? throw new InvalidOperationException("No entities found in the collection.");
    }

    public async Task<TDomain> GetByIdAsync(Guid id)
    {
        var filter = Builders<TDocument>.Filter.Eq(e => e.ID, id.ToString());
        var entity = await Collection.Find(filter).FirstOrDefaultAsync().ConfigureAwait(false);
        return Mapper.Map<TDomain>(entity) ?? throw new KeyNotFoundException($"Entity with ID {id} not found.");
    }

    public async Task<TDomain> AddAsync(TDomain entity)
    {
        var entityDocument = Mapper.Map<TDocument>(entity) ?? throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");    
        await Collection.InsertOneAsync(entityDocument).ConfigureAwait(false);
        return entity;
    }

    public async Task<TDomain> UpdateAsync(TDomain entity)
    {
        var entityDocument = Mapper.Map<TDocument>(entity) ?? throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
        var filter = Builders<TDocument>.Filter.Eq(e => e.ID, entity.Id.ToString());
        await Collection.ReplaceOneAsync(filter,entityDocument).ConfigureAwait(false);
        return entity;
    }

    public async Task<TDomain> DeleteAsync(TDomain entity)
    {
        var filter = Builders<TDocument>.Filter.Eq(e => e.ID, entity.Id.ToString());
        await Collection.DeleteOneAsync(filter).ConfigureAwait(false);
        return entity;
    }
}
