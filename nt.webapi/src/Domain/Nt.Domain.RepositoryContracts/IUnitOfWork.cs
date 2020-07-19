using Nt.Domain.Entities.User;

namespace Nt.Domain.RepositoryContracts
{
    public interface IUnitOfWork
    {
        IGenericRepository<UserProfileEntity> UserProfileRepository { get; }
    }
}
