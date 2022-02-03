using AuthService.Data.Repository;

namespace AuthService.Data.Database;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    void SaveChanges();
    void BeginTransaction();
}
public class UnitOfWork : IUnitOfWork
{
    private Lazy<UserRepository> _userRepository;
    private IDataContext _dataContext;
    public IUserRepository UserRepository => _userRepository.Value;

    public UnitOfWork(IDataContext dataContext)
    {
        _dataContext = dataContext;
        _userRepository = new Lazy<UserRepository>(() => new UserRepository(this));
    }

    public void BeginTransaction()
    {
        _dataContext.BeginTransaction();
    }

    public void SaveChanges()
    {
        _dataContext.Commit();
    }

    public void Dispose() => Dispose(true);

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dataContext.Dispose();
        }
    }
}
