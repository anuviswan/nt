using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Entities;
using MovieService.Data;
using MovieService.Data.Interfaces.Entities;
using MovieService.Data.Seed;
using MovieService.Service.Interfaces.Dtos;

namespace MovieService.Api;

// We do not use the .Net Module initializers as we do not have control over 
// when exactly it would be run. 
public class ModuleInitializer
{
    private readonly DatabaseInitializer _dbInitializer;
    public ModuleInitializer(DatabaseInitializer dbInitializer)
    {
        _dbInitializer = dbInitializer;
    }
    public async Task Initialize()
    {
        await ExecuteDatabaseSeeding().ConfigureAwait(false);
    }

    private async Task ExecuteDatabaseSeeding()
    {
        await _dbInitializer.Initialize();
    }
}

public class DatabaseInitializer
{
    private readonly DatabaseSettings _databaseSettings;
    private readonly IMongoDatabase _database;
    public DatabaseInitializer(IOptions<DatabaseSettings> databaseSettings)
    {

        _databaseSettings = databaseSettings.Value;

        _databaseSettings.ConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings__nt-movieservice-db") ?? _databaseSettings.ConnectionString;

        DB.InitAsync(_databaseSettings.DatabaseName,MongoClientSettings.FromConnectionString(_databaseSettings.ConnectionString));
        _database = DB.Database(_databaseSettings.DatabaseName);
    }

    public async Task Initialize()
    {
        await IndexInitializer.Setup();

        var collection = await GetCollection(_databaseSettings.MovieCollectionName);
        bool exists = await collection.Find(FilterDefinition<MovieEntity>.Empty)
                               .AnyAsync();

        if (collection is not null && !exists)
        {
            var documents = Seed.Movies;
            await collection.InsertManyAsync(documents).ConfigureAwait(false);
        }
    }


    private async Task<IMongoCollection<MovieEntity>?> GetCollection(string collectionName)
    {
        var filter = new BsonDocument("name", collectionName);
        var collections = await _database.ListCollectionNamesAsync(new ListCollectionNamesOptions
        {
            Filter = filter
        });

        return _database.GetCollection<MovieEntity>(_databaseSettings.MovieCollectionName);
    }
}

public static class IndexInitializer
{
    public static async Task Setup()
    {
        await DB.Index<MovieEntity>()
                .Key(x => x.Title, KeyType.Text)
                //.Key(x => x.Description, KeyType.Text) TODO For Future
                .CreateAsync();
    }
}


