using Microsoft.Extensions.Options;
using MongoDB.Bson;
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

    public IQueryable<MovieEntity> Search(string searchTerm)
    {
        try
        {
            return DB.Queryable<MovieEntity>()
                                    .Where(m => m.Title.ToLower().Contains(searchTerm.ToLower()));

            // Use a regex for case-insensitive search

        }
        catch (Exception e )
        {

            throw e;
        }
        //return await DB.Queryable<MovieEntity>().Where(x => x.Regex(c => c.Title, new BsonRegularExpression(searchTerm, "i")));
        
      
    }
}
