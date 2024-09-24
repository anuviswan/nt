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
        if (!await CheckIfCollectionExists(_databaseSettings.MovieCollectionName))
        {
            var collection = _database.GetCollection<MovieEntity>(_databaseSettings.MovieCollectionName);

            var documents = new[]
            {
                new MovieEntity
                {
                    Title = "Yodha",
                    Language = "Malayalam",
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
                    Title = "Manichitrathazu",
                    Language = "Malayalam",
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
                    Title = "Amaram",
                    Language = "Malayalam",
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

