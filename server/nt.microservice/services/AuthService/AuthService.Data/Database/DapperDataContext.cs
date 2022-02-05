using AuthService.Data.Repository;
using AuthService.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AuthService.Data.Database;

public class DapperDataContext : IDataContext
{
    private IDbTransaction _dbTransaction;

    private Lazy<UserRepository> _userRepository;

    public DapperDataContext(string connectionString)
    {
        Connection = new SqlConnection(connectionString);
        _userRepository = new Lazy<UserRepository>(() => new UserRepository(Connection));
    }

    public IDbConnection Connection { get; }

    public IUserRepository UserRepository => _userRepository.Value;

    public void BeginTransaction()
    {
        _dbTransaction = Connection.BeginTransaction();
    }

    public void Commit()
    {
        try
        {
            _dbTransaction.Commit();
        }
        catch (Exception)
        {
            _dbTransaction.Rollback();
        }
    }

    public void Dispose() => Dispose(true);

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dbTransaction?.Dispose();
            Connection?.Dispose();
        }
    }
}
