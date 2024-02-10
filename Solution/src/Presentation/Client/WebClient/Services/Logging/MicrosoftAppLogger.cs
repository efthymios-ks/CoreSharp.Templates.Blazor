using Microsoft.Extensions.Logging;
using System;
using WebClient.Enums;
using WebClient.Services.Interfaces.Logging;

namespace WebClient.Services.Logging;

internal sealed class MicrosoftAppLogger : IAppLogger
{
    // Fields 
    private readonly ILogger _logger;

    // Constructors 
    public MicrosoftAppLogger(ILogger logger)
        => _logger = logger;

    // Methods
    public void Debug(string message, params object[] args)
        => _logger.LogDebug(message, args: args);

    public void Info(string message, params object[] args)
        => _logger.LogInformation(message, args: args);

    public void Warning(string message, params object[] args)
        => _logger.LogWarning(message, args: args);

    public void Error(string message, params object[] args)
        => Error(exception: null, message, args: args);

    public void Error(Exception exception, string message, params object[] args)
        => _logger.LogError(exception, message, args: args);

    public void IsEnabled(AppLogLevel level)
        => _logger.IsEnabled(ToMicrosoftLogLevel(level));

    private static LogLevel ToMicrosoftLogLevel(AppLogLevel level)
        => level switch
        {
            AppLogLevel.Debug => LogLevel.Debug,
            AppLogLevel.Info => LogLevel.Information,
            AppLogLevel.Warning => LogLevel.Warning,
            AppLogLevel.Error => LogLevel.Error,
            _ => throw new ArgumentOutOfRangeException(nameof(level))
        };
}