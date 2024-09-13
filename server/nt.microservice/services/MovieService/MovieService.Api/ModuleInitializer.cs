using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MovieService.Data;

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

        var client = new MongoClient(_databaseSettings.ConnectionString);
        _database = client.GetDatabase(_databaseSettings.DatabaseName);
    }

    public async Task Initialize()
    {
        if (!await CheckIfCollectionExists(_databaseSettings.MovieCollectionName))
        {
            var collection = _database.GetCollection<BsonDocument>(_databaseSettings.MovieCollectionName);

            var documents = new[]
            {
                new BsonDocument{{"title","DemoMovie"}}
            };

            await collection.InsertManyAsync(documents).ConfigureAwait(false);
        }
    }


    private async Task<bool> CheckIfCollectionExists(string collectionName)
    {
        var filter = new BsonDocument("name", collectionName);
        var collections = await _database.ListCollectionNamesAsync(new ListCollectionNamesOptions
        {
            Filter = filter
        });

        return await collections.AnyAsync();
    }
}

