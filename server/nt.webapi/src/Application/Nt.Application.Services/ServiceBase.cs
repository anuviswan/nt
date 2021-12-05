using Nt.Domain.RepositoryContracts;

namespace Nt.Application.Services;
public class ServiceBase
{
    protected IUnitOfWork UnitOfWork { get;}
    public ServiceBase(IUnitOfWork unitOfWork) => UnitOfWork = unitOfWork;
}
