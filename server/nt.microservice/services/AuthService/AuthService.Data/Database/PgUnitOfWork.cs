﻿using AuthService.Data.Interfaces.Database;
using AuthService.Data.Interfaces.Repository;
using System.Data;

namespace AuthService.Data.Database;

public class PgUnitOfWork : IUnitOfWork
{
    private IDataContext _dataContext;
    public IUserRepository UserRepository => _dataContext.UserRepository;

    public IDbConnection Connection => _dataContext.Connection;

    public PgUnitOfWork(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public void BeginTransaction()
    {
        _dataContext.BeginTransaction();
    }

    public void SaveChanges()
    {
        _dataContext.Commit();
    }

    public void Dispose() => Dispose(true);

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dataContext.Dispose();
        }
    }

    
}
