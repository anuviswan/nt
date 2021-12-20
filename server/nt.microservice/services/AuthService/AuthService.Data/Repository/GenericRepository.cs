﻿using AuthService.Data.Database;
using AuthService.Domain.Entities;

namespace AuthService.Data.Repository;
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity, new()
{
    protected readonly UserDbContext UserDbContext;

    public GenericRepository(UserDbContext userDbContext) => UserDbContext = userDbContext;

    public IEnumerable<TEntity> GetAll() => UserDbContext.Set<TEntity>();

    public async Task<TEntity> GetByIdAsync(long id)
    {
        return await UserDbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await UserDbContext.AddAsync(entity);
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
}