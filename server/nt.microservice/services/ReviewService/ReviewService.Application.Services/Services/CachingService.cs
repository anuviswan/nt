using Microsoft.Extensions.Options;
using ReviewService.Application.Interfaces.Services;
using StackExchange.Redis;
using System.Text.Json;

namespace ReviewService.Application.Services.Services;

public class CachingService(IConnectionMultiplexer connectionMultiplexer, int timeOutInMinutes) : ICachingService
{
    public IConnectionMultiplexer ConnectionMultiplexer => connectionMultiplexer;
    public IDatabase Database => ConnectionMultiplexer.GetDatabase();

    public TimeSpan ExpirationTime { get; } = TimeSpan.FromMinutes(timeOutInMinutes);

    public async Task StringSetAsync<T>(string key, T value) where T : class
    {
        await Database.StringSetAsync(key, JsonSerializer.Serialize(value), ExpirationTime);
    }

    public async Task SortedSetAsync<T>(string key, T value, double score) where T : class
    {
        var serializedValue = JsonSerializer.Serialize(value);
        await Database.SortedSetAddAsync(key, serializedValue, score); //DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        await Database.KeyExpireAsync(key, ExpirationTime);
    }
    public async Task<T?> StringGetAsync<T>(string key) where T : class
    {
        var value = await Database.StringGetAsync(key);
        return value.IsNullOrEmpty ? null : JsonSerializer.Deserialize<T>(value!);
    }

    public async Task<IEnumerable<T>> SortedSetRangeByScoreAsync<T>(string key, int count) where T : class
    {
        var values = await Database.SortedSetRangeByScoreAsync(key, order: Order.Descending, take: count);
        return values.Select(value => JsonSerializer.Deserialize<T>(value!.ToString())).Where(value => value != null)!;
    }

    public async Task<IEnumerable<string>> SortedSetRangeByScoreAsync(string key, int count)
    {
        var values = await Database.SortedSetRangeByScoreAsync(key, order: Order.Descending, take: count);
        return values.Select(value => value.ToString()!);
    }
}
