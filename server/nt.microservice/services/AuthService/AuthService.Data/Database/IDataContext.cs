using Microsoft.Data.SqlClient;
using System.Data;

namespace AuthService.Data.Database;
public interface IDataContext:IDisposable
{
    IDbConnection Connection { get; }
    void BeginTransaction();
    void Commit();

}

public class DapperDataContext : IDataContext
{
    private IDbTransaction _dbTransaction;
    public DapperDataContext(string connectionString)
    {
        Connection = new SqlConnection(connectionString);
    }

    public IDbConnection Connection { get; }

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
            _dbTransaction.Dispose();
            Connection.Dispose();
        }
    }
}
