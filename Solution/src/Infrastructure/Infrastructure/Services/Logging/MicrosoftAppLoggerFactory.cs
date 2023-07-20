using CoreSharp.Templates.Blazor.Application.Services.Interfaces.Logging;
using Microsoft.Extensions.Logging;
using System;

namespace CoreSharp.Templates.Blazor.Infrastructure.Services.Logging;

internal sealed class MicrosoftAppLoggerFactory : IAppLoggerFactory
{
    // Fields
    private readonly ILoggerFactory _loggerFactory;

    // Constructors 
    public MicrosoftAppLoggerFactory(ILoggerFactory loggerFactory)
        => _loggerFactory = loggerFactory;

    // Properties 
    public IAppLogger CreateLogger<TCategoryType>()
        => CreateLogger(typeof(TCategoryType));

    public IAppLogger CreateLogger(Type categoryNameType)
    {
        ArgumentNullException.ThrowIfNull(categoryNameType);

        var logger = _loggerFactory.CreateLogger(categoryNameType);
        return new MicrosoftAppLogger(logger);
    }
}