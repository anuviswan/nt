﻿
using System.Globalization;

namespace MovieService.Api.Loggers;
public class FileLogger : ILogger
{
    private readonly string _filePath;
    private readonly LogLevel _minLogLevel;
    private object _lock = new object();
    private readonly CultureInfo _ci = CultureInfo.InvariantCulture;
    public FileLogger(string filePath, LogLevel minLogLevel)
    {
        (_filePath, _minLogLevel) = (filePath, minLogLevel);

        EnsurePathExists();
    }

    private void EnsurePathExists()
    {
        var directoryPath = Path.GetDirectoryName(_filePath);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath!);
        }
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

    // Check minimum log levels
    public bool IsEnabled(LogLevel logLevel) => logLevel >= _minLogLevel;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        ArgumentNullException.ThrowIfNull(nameof(formatter));

        if (!IsEnabled(logLevel)) return;

        var message = formatter(state, exception);
        var logMessage = $"{DateTime.Now.ToString("dd/mm/yyyy hh:mm:ss.ff", _ci)}: [{logLevel.ToString()}] : {message}";
        lock (_lock)
        {

            File.AppendAllText(_filePath, logMessage);
        }

    }
}
