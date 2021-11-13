using Nt.Domain.Entities.Movie;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts.Movie;

namespace Nt.Domain.RepositoryContracts;
public interface IUnitOfWork
{
    IGenericRepository<UserProfileEntity> UserProfileRepository { get; }
    IGenericRepository<MovieEntity> MovieRepository { get; }
    IReviewRepository ReviewRepository { get; }
}
