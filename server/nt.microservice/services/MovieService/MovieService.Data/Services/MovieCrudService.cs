using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;
using MovieService.Domain.Entities;

namespace MovieService.Data.Services;

public interface IMovieCrudService
{

}
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

}
