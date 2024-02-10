using System;
using WebClient.Enums;
using WebClient.Services.Interfaces.Logging;

namespace WebClient.Services.Logging;

internal sealed class MicrosoftAppLogger<TCategoryName> : IAppLogger<TCategoryName>
{
    // Fields
    private readonly IAppLogger _appLogger;

    // Constructors 
    public MicrosoftAppLogger(IAppLoggerFactory appLoggerFactory)
        => _appLogger = appLoggerFactory.CreateLogger<TCategoryName>();

    // Methods
    public void Debug(string message, params object[] args)
        => _appLogger.Debug(message, args: args);

    public void Info(string message, params object[] args)
        => _appLogger.Info(message, args: args);

    public void Warning(string message, params object[] args)
        => _appLogger.Warning(message, args: args);

    public void Error(string message, params object[] args)
        => Error(exception: null, message, args: args);

    public void Error(Exception exception, string message, params object[] args)
        => _appLogger.Error(exception, message, args: args);

    public void IsEnabled(AppLogLevel level)
        => _appLogger.IsEnabled(level);
}
