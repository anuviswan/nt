using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Entities;
using MovieService.Data;
using MovieService.Data.Interfaces.Entities;
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
            var documents = new[]
            {
                new MovieEntity
                {
                    ID = "556fa735-8d64-4dad-ac9f-b71348c1c683",
                    Title = "Yodha",
                    MovieLanguage = "Malayalam",
                    ReleaseDate = new DateOnly(1992, 9, 3).ToDateTime(TimeOnly.MinValue),
                    Crew = new Dictionary<string, List<PersonEntity>>
                    {
                        ["Director"] = [new ("Sangeeth Sivan")],
                        ["Music Director"] = [new ("A R Rahman")],
                    },
                    Cast = new List<PersonEntity>
                    {
                        new ("Mohanlal"),
                        new("Jagathy Sreekumar"),
                        new("Madhu")
                    }
                },

                new MovieEntity
                {
                    ID = "c0ed3691-e197-4ce4-9580-7a9d08fea416",
                    Title = "Manichitrathazu",
                    MovieLanguage = "Malayalam",
                    ReleaseDate = new DateOnly(1993, 12, 25).ToDateTime(TimeOnly.MinValue),
                    Crew = new Dictionary<string, List<PersonEntity>>
                    {
                        ["Director"] = [new("Fazil")],
                        ["Story"] = [new("Madhu Muttam")]
                    },
                    Cast = new List<PersonEntity>
                    {
                        new("Mohanlal"),
                        new("Suresh Gopi"),
                        new("Shobana")
                    }
                },

                new MovieEntity
                {
                    ID = "4c7efcd9-66ad-4b42-9b7d-5933d47f17e9",
                    Title = "Amaram",
                    MovieLanguage = "Malayalam",
                    ReleaseDate = new DateOnly(1991, 2, 1).ToDateTime(TimeOnly.MinValue),
                    Crew = new Dictionary<string, List<PersonEntity>>
                    {
                        ["Director"] = [new ("Bharathan")],
                        ["Story"] = [new ("A K Lohithadas")]
                    },
                    Cast = new List<PersonEntity>
                    {
                        new ("Mammootty"),
                        new ("Ashokan"),
                        new ("Maathu"),
                        new ("Murali")
                    }
                },
                
            }.AsEnumerable();

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


