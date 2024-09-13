using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;

namespace MovieService.Service.Tests.ServicesTests;

public class ServiceTestsBase
{
    protected ILogger<T> CreateNullLogger<T>()
    {
        var loggerFactory = new NullLoggerFactory();
        return loggerFactory.CreateLogger<T>();
    }
}
