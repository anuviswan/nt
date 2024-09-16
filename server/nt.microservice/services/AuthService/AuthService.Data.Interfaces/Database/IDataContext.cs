using AuthService.Data.Interfaces.Repository;
using System.Data;

namespace AuthService.Data.Interfaces.Database;
public interface IDataContext : IDisposable
{
    IDbConnection Connection { get; }
    void BeginTransaction();
    void Commit();

    IUserRepository UserRepository { get; }

}
