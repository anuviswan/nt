﻿using Nt.Domain.Entities.Entities;
using System.Linq.Expressions;

namespace Nt.Domain.RepositoryContracts;
public interface IGenericRepository<TEntity> where TEntity : class, IBaseEntity
{
    Task<TEntity> CreateAsync(TEntity data);
    Task<IEnumerable<TEntity>> GetAsync();
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<bool> UpdateAsync(TEntity data);
}