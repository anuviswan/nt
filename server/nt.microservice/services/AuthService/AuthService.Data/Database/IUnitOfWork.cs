namespace AuthService.Data.Database;
internal interface IUnitOfWork:IDisposable
{
    void SaveChanges();
}
