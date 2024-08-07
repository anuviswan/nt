﻿using MovieService.Api.Settings;

namespace MovieService.Api.Loggers;
public class CustomLoggerProvider : ILoggerProvider
{
    private const string DateTimeSuffixFormat = "yyyyMMdd";
    private readonly LoggerConfiguration _configuration;
    public CustomLoggerProvider(LoggerConfiguration configuration)
    {
        _configuration = configuration;
    }
    public ILogger CreateLogger(string categoryName)
    {
        var fileInfo = new FileInfo(_configuration.FileName!);
        var directoryName = fileInfo!.Directory!.ToString();
        var fileName = $"{DateTime.UtcNow.ToString(DateTimeSuffixFormat)}{fileInfo.Name}.log";
        var filePath = Path.Combine(directoryName, fileName);
        return new FileLogger(filePath, _configuration.LogLevel.HasValue ? _configuration.LogLevel.Value : LogLevel.Information);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}