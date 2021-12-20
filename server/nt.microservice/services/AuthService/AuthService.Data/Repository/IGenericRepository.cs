using AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Data.Repository;
public interface IGenericRepository<TEntity> where TEntity : class, IEntity, new()
{
    IEnumerable<TEntity> GetAll();
    Task<TEntity> GetByIdAsync(long id);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);
}