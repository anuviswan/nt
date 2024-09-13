using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MovieService.Data.Interfaces.Services;
using MovieService.Domain.Entities;

namespace MovieService.Data.Services;


public class MovieCrudService : IMovieCrudService
{
    private readonly IMongoCollection<Movie> _moviesCollection;

    public MovieCrudService(
        IOptions<DatabaseSettings> bookStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            bookStoreDatabaseSettings.Value.DatabaseName);

        _moviesCollection = mongoDatabase.GetCollection<Movie>(
            bookStoreDatabaseSettings.Value.MovieCollectionName);
    }

    public async Task CreateAsync(Movie newBook) =>
        await _moviesCollection.InsertOneAsync(newBook);
}
