using AuthService.Data.Interfaces.Database;

namespace AuthService.Data.Database;

public interface IUnitOfWorkFactory
{
    IUnitOfWork CreateUnitOfWork();
}
