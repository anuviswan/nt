using Microsoft.EntityFrameworkCore;
namespace UserService.Data.Repository;
public class GenericRepository<TEntity,TDbContext> : IGenericRepository<TEntity,TDbContext>, IDisposable
    where TEntity : class, IEntity, new()
    where TDbContext : DbContext
{
    protected readonly TDbContext DatabaseContext;

    public GenericRepository(TDbContext dbContext) => DatabaseContext = dbContext;

    public IEnumerable<TEntity> GetAll() => DatabaseContext.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(long id)
    {
        return await DatabaseContext.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
    }

    public async Task<TEntity> AddAsync(TEntity entity) 
    {
        ArgumentNullException.ThrowIfNull(entity);
        var entities = DatabaseContext.Set<TEntity>();
        
        await entities.AddAsync(entity).ConfigureAwait(false);
        DatabaseContext.Entry(entity).State = EntityState.Added;
        await DatabaseContext.SaveChangesAsync().ConfigureAwait(false);

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        DatabaseContext.Update(entity);
        await DatabaseContext.SaveChangesAsync().ConfigureAwait(false);

        return entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        var itemToRemove = await DatabaseContext.Set<TEntity>().FindAsync(entity.Id).ConfigureAwait(false);
        if (itemToRemove != null)
        {
            DatabaseContext.Remove<TEntity>(itemToRemove);
            await DatabaseContext.SaveChangesAsync().ConfigureAwait(false);
            return itemToRemove;
        }

        return await Task.FromException<TEntity>(new ArgumentOutOfRangeException());
    }

    private bool _disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            DatabaseContext.Dispose();
        }
        _disposed = true;
    }
    public void Dispose()
    {
        // Dispose of unmanaged resources.
        Dispose(true);
        // Suppress finalization.
        GC.SuppressFinalize(this);
    }
}
