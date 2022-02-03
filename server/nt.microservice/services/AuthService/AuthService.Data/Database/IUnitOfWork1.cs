using AuthService.Data.Repository;

namespace AuthService.Data.Database;

internal interface IUnitOfWork:IDisposable
{
    void SaveChanges();
    IUserRepository UserRepository { get; } 
}
