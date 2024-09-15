using AuthService.Data.Interfaces.Repository;
using System.Data;

namespace AuthService.Data.Interfaces.Database;
public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    void SaveChanges();
    void BeginTransaction();
    IDbConnection Connection { get; }
}
