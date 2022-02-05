using AuthService.Data.Repository;
using AuthService.Domain.Entities;
using System.Data;

namespace AuthService.Data.Database;
public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    void SaveChanges();
    void BeginTransaction();
    IDbConnection Connection { get; }
}
