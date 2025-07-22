using ReviewService.Application.Interfaces.Services;
using StackExchange.Redis;
using System.Text.Json;

namespace ReviewService.Application.Services.Services;

public class CachingService(IConnectionMultiplexer connectionMultiplexer) : ICachingService
{
    public IConnectionMultiplexer ConnectionMultiplexer => connectionMultiplexer;
    public IDatabase Database => ConnectionMultiplexer.GetDatabase();
    public void Set<T>(string key, T value, TimeSpan expirationTime) where T : class
    {
        Database.StringSet(key, JsonSerializer.Serialize(value), expirationTime);
    }
    public T? Get<T>(string key) where T : class
    {
        var value = Database.StringGet(key);
        return value.IsNullOrEmpty ? null : JsonSerializer.Deserialize<T>(value!);
    }
}
