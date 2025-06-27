using Microsoft.Extensions.Options;
using MongoDB.Entities;
using MovieService.Data.Interfaces.Entities;
using MovieService.Data.Interfaces.Services;

namespace MovieService.Data.Services;
public class MovieCrudService : IMovieCrudService
{
    public MovieCrudService(
        IOptions<DatabaseSettings> bookStoreDatabaseSettings)
    {
        DB.InitAsync(bookStoreDatabaseSettings.Value.ConnectionString);
        DB.Database(bookStoreDatabaseSettings.Value.DatabaseName);
    }

    public async Task CreateAsync(MovieEntity newBook) =>
        await newBook.SaveAsync();

    public async IAsyncEnumerable<MovieEntity> SearchAsync(string searchTerm)
    {
        var cursor = await DB.Find<MovieEntity>()
                             .Match(Search.Full, searchTerm)
                             .ExecuteCursorAsync();

        while (await cursor.MoveNextAsync())
        {
            foreach (var movie in cursor.Current)
            {
                yield return movie;
            }
        }
    }

    public async IAsyncEnumerable<MovieEntity> GetRecentMovies(int count = 10)
    {
        if (count <= 0)
            yield break;
        var cursor = await DB.Find<MovieEntity>()
                             .Sort(x=>x.Descending(x => x.ReleaseDate))
                             .Limit(count)
                             .ExecuteCursorAsync();
        while (await cursor.MoveNextAsync())
        {
            foreach (var movie in cursor.Current)
            {
                yield return movie;
            }
        }
    }
}
