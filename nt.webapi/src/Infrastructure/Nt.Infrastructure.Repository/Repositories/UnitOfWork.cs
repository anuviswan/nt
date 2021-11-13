using Nt.Domain.Entities.Settings;
using Nt.Domain.RepositoryContracts;
using Nt.Infrastructure.Data.Repositories.Movie;

namespace Nt.Infrastructure.Data.Repositories;
public class UnitOfWork : IUnitOfWork
{
    protected readonly MongoClient _mongoClient;
    protected readonly IMongoDatabase _mongoDatabase;
    private Lazy<GenericRepository<UserProfileEntity>> _userProfileRepository;
    private Lazy<GenericRepository<MovieEntity>> _movieRepository;
    private Lazy<ReviewRepository> _reviewRepository;

    public UnitOfWork(IDatabaseSettings settings)
    {
        _mongoClient = new (settings.ConnectionString);
        _mongoDatabase = _mongoClient.GetDatabase(settings.DatabaseName);

        _userProfileRepository = new (() => new (_mongoDatabase));
        _movieRepository = new (() => new (_mongoDatabase));
        _reviewRepository = new (() => new (_mongoDatabase));
    }


    public IGenericRepository<UserProfileEntity> UserProfileRepository => _userProfileRepository.Value;
    public IGenericRepository<MovieEntity> MovieRepository => _movieRepository.Value;
    public IReviewRepository ReviewRepository => _reviewRepository.Value;

}
