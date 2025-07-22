namespace ReviewService.Application.Interfaces.Services;

public interface ICachingService
{
    void Set<T>(string key, T value, TimeSpan expirationTime) where T : class;
    T? Get<T>(string key) where T : class;
}
