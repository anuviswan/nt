namespace AuthService.Data.Database;

public interface IUnitOfWorkFactory
{
    IUnitOfWork CreateUnitOfWork();
}
