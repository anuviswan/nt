using ReviewService.Domain.Entities;

namespace ReviewService.Application.Interfaces.Repository;

public interface IGenericRepository<TEntity> where TEntity : class, IEntity, new()
{
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity> GetByIdAsync(long id);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);
}

public interface ReviewRepository : IGenericRepository<Review>
{
    Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(Guid movieId);
    Task<IEnumerable<Review>> GetReviewsByUserIdAsync(Guid userId);
    Task<IEnumerable<Review>> GetReviewsByRatingAsync(int rating);
    Task<IEnumerable<Review>> GetReviewsByDateRangeAsync(DateTime startDate, DateTime endDate);
}