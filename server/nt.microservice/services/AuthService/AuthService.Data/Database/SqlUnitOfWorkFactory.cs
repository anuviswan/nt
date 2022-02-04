using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Data.Database;
public class SqlUnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly string _connectionString;
    private IDataContext _dataContext;
    public SqlUnitOfWorkFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDataContext GetOrCreateContext()
    {
        if(_dataContext?.Connection?.State == System.Data.ConnectionState.Closed)
        {
            _dataContext = new DapperDataContext(_connectionString);
            _dataContext.Connection.Open();
        }
        return _dataContext;
    }
    public IUnitOfWork CreateUnitOfWork()
    {
        return new SqlUnitOfWork(GetOrCreateContext());
    }
}