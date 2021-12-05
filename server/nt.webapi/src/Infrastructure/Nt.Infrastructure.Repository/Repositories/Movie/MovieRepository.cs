namespace Nt.Infrastructure.Data.Repositories.Movie;
public class MovieRepository : GenericRepository<MovieEntity>, IMovieRepository
{
    public MovieRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {

    }
}
