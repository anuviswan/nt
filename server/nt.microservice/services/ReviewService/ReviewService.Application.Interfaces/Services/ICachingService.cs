namespace ReviewService.Application.Interfaces.Services;

public interface ICachingService
{
    Task SetAsync<T>(string key, T value, TimeSpan expirationTime) where T : class;
    Task<T?> GetAsync<T>(string key) where T : class;
}
