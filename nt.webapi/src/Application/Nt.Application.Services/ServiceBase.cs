using Nt.Domain.RepositoryContracts;

namespace Nt.Application.Services
{
    public class ServiceBase
    {
        protected IUnitOfWork _unitOfWork;
        public ServiceBase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
    }
}
