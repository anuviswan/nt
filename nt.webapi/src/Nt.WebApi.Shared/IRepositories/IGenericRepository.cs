using Nt.WebApi.Shared.Entities;
using Nt.WebApi.Shared.Settings;
using System;
using System.Collections.Generic;

namespace Nt.WebApi.Shared.IRepositories
{
    public interface IGenericRepository<TEntity, TDataContext> where TEntity : class, IBaseEntity
                                                               where TDataContext : IDatabaseSettings
    {
        TEntity Create(TEntity data);
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
    }
}
