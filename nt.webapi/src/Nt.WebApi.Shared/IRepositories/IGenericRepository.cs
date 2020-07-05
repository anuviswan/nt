using Nt.WebApi.Shared.Entities;
using Nt.WebApi.Shared.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nt.WebApi.Shared.IRepositories
{
    public interface IGenericRepository<TEntity, TDataContext> where TEntity : class, IBaseEntity
                                                               where TDataContext : IDatabaseSettings
    {
        Task<TEntity> CreateAsync(TEntity data);
        Task<IEnumerable<TEntity>> GetAsync();
        Task<IEnumerable<TEntity>> GetAsync(Func<TEntity, bool> predicate);
    }
}
