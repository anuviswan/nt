using AuthService.Data.Interfaces.Database;
using AuthService.Data.Interfaces.Repository;
using AuthService.Data.Repository;
using Npgsql;
using System.Data;

namespace AuthService.Data.Database;

public class DapperDataContext : IDataContext
{
    private IDbTransaction? _dbTransaction;
    
    private Lazy<UserRepository> _userRepository;

    public DapperDataContext(string connectionString)
    {
        Connection = new NpgsqlConnection(connectionString);
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
        ArgumentNullException.ThrowIfNull(_dbTransaction, nameof(_dbTransaction));
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
