using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
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
}
