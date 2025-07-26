namespace ReviewService.Application.Interfaces.Services;

public interface ICachingService
{
    TimeSpan ExpirationTime { get; }
    Task StringSetAsync<T>(string key, T value) where T : class;
    Task SortedSetAsync<T>(string key, T value, double score) where T : class;
    Task<T?> StringGetAsync<T>(string key) where T : class;
    Task<IEnumerable<T>> SortedSetRangeByScoreAsync<T>(string key, int count) where T : class;
}
