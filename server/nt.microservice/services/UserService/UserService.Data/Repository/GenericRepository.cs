namespace UserService.Data.Repository;
public class GenericRepository<TEntity> : IGenericRepository<TEntity>, IDisposable where TEntity : class, IEntity, new()
{
    protected readonly UserManagementDbContext UserDbContext;

    public GenericRepository(UserManagementDbContext userDbContext) => UserDbContext = userDbContext;

    public IEnumerable<TEntity> GetAll() => UserDbContext.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(long id)
    {
        return await UserDbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> AddAsync(TEntity entity) 
    {
        ArgumentNullException.ThrowIfNull(entity);
        var entities = UserDbContext.Set<TEntity>();
        await entities.AddAsync(entity);
        await UserDbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        UserDbContext.Update(entity);
        await UserDbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        var itemToRemove = await UserDbContext.Set<TEntity>().FindAsync(entity.Id);
        UserDbContext.Remove(itemToRemove);
        await UserDbContext.SaveChangesAsync();
        return itemToRemove;
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
            UserDbContext.Dispose();
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
