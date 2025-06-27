namespace MovieService.Data.Interfaces.Services;

public interface IMovieCrudService
{
    Task CreateAsync(MovieEntity newBook);
    IAsyncEnumerable<MovieEntity> SearchAsync(string searchTerm);
    IAsyncEnumerable<MovieEntity> GetRecentMovies(int count = 10);
}
