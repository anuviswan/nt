using Nt.Domain.Entities.Movie;
using Nt.Domain.Entities.User;

namespace Nt.Domain.RepositoryContracts
{
    public interface IUnitOfWork
    {
        IGenericRepository<UserProfileEntity> UserProfileRepository { get; }
        IGenericRepository<MovieEntity> MovieRepository { get; }
        IGenericRepository<ReviewEntity> ReviewRepository { get; }
    }
}
