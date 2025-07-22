using ReviewService.Application.Interfaces.Services;
using StackExchange.Redis;
using System.Text.Json;

namespace ReviewService.Application.Services.Services;

public class CachingService(IConnectionMultiplexer connectionMultiplexer) : ICachingService
{
    public IConnectionMultiplexer ConnectionMultiplexer => connectionMultiplexer;
    public IDatabase Database => ConnectionMultiplexer.GetDatabase();
    public async Task SetAsync<T>(string key, T value, TimeSpan expirationTime) where T : class
    {
        await Database.StringSetAsync(key, JsonSerializer.Serialize(value), expirationTime);
    }
    public async Task<T?> GetAsync<T>(string key) where T : class
    {
        var value = await Database.StringGetAsync(key);
        return value.IsNullOrEmpty ? null : JsonSerializer.Deserialize<T>(value!);
    }
}
