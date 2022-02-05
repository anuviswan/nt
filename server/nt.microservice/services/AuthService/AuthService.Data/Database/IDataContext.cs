using AuthService.Data.Repository;
using System.Data;

namespace AuthService.Data.Database;
public interface IDataContext:IDisposable
{
    IDbConnection Connection { get; }
    void BeginTransaction();
    void Commit();

    IUserRepository UserRepository { get; }

}
