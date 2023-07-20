using System;

namespace CoreSharp.Templates.Blazor.Application.Services.Interfaces.Logging;

public interface IAppLoggerFactory
{
    // Methods 
    IAppLogger CreateLogger<TCategoryType>();
    IAppLogger CreateLogger(Type categoryNameType);
}