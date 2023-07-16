using Microsoft.EntityFrameworkCore;

namespace UserService.Data.Repository;
public interface IGenericRepository<TEntity,TDbContext> 
    where TEntity : class, IEntity, new()
    where TDbContext : DbContext
{
    IEnumerable<TEntity> GetAll();
    Task<TEntity?> GetByIdAsync(long id);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);
}
