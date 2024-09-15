using AuthService.Data.Interfaces.Repository;
using DapperExtensions;
using System.Data;

namespace AuthService.Data.Repository;
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity, new()
{
    protected readonly IDbConnection Connection;
    public GenericRepository(IDbConnection connection)
    {
        Connection = connection;
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await Connection.GetListAsync<TEntity>(buffered: true);

    }

    public async Task<TEntity> GetByIdAsync(long id)
    {
        return await Connection.GetAsync<TEntity>(id);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await Connection.InsertAsync<TEntity>(entity);

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await Connection.UpdateAsync(entity);
        return entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await Connection.DeleteAsync(entity);
        return entity;
    }
}
