using Microsoft.Extensions.Options;
using MongoDB.Entities;
using MovieService.Data.Interfaces.Entities;
using MovieService.Data.Interfaces.Services;
using System.Net.Http.Headers;

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
                             .Project(movie => new MovieEntity
                             {
                                    ID = movie.ID,
                                    Title = movie.Title,
                                    MovieLanguage = movie.MovieLanguage,
                                    ReleaseDate = movie.ReleaseDate,
                                    Synopsis = movie.Synopsis,
                                    Cast = movie.Cast.Select(c => new PersonEntity { Name = c.Name }).ToList(),
                                    Crew = movie.Crew.ToDictionary(
                                        kvp => kvp.Key,
                                        kvp => kvp.Value.Select(p => new PersonEntity { Name = p.Name }).ToList()) 
                             })
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

    public async Task<MovieEntity?> GetMovieByIdAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
            return null;

        return await DB.Find<MovieEntity>()
                       .Match(x => x.ID == id)
                       .ExecuteFirstAsync();
    }
}
