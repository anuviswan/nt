using Nt.WebApi.Models.Settings;
using System;
using System.Collections.Generic;

namespace Nt.WebApi.Interfaces.Repository
{
    public interface IGenericRepository<TEntity, TDataContext> where TEntity : class, IBaseEntity
                                                               where TDataContext : IDatabaseSettings
    {
        TEntity Create(TEntity data);
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
    }
}
