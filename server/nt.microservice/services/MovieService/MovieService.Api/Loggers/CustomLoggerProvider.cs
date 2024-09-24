﻿using Microsoft.Extensions.Options;
using MovieService.Api.Settings;
using Path = System.IO.Path;

namespace MovieService.Api.Loggers;
public class CustomLoggerProvider : ILoggerProvider
{
    private const string DateTimeSuffixFormat = "yyyyMMdd";
    private readonly LoggerConfiguration _configuration;
    public CustomLoggerProvider(IOptions<LoggerConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }
    public ILogger CreateLogger(string categoryName)
    {
        var fileInfo = new FileInfo(_configuration.FileName!);
        var directoryName = fileInfo!.Directory!.ToString();
        var fileName = $"{DateTime.UtcNow.ToString(DateTimeSuffixFormat)}{fileInfo.Name}";
        var filePath = Path.Combine(directoryName, fileName);
        return new FileLogger(filePath, _configuration.LogLevel.HasValue ? _configuration.LogLevel.Value : LogLevel.Information);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}