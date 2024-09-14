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

    public async Task CreateAsync(Movie newBook) =>
        await newBook.SaveAsync();
}
