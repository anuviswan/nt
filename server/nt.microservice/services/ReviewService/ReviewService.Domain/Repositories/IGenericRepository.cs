using ReviewService.Domain.Entities;

namespace ReviewService.Domain.Repositories;

public interface IGenericRepository<TEntity> where TEntity : class, IEntity, new()
{
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity> GetByIdAsync(Guid id);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);
}
