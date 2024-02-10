using System;

namespace WebClient.Services.Interfaces.Logging;

public interface IAppLoggerFactory
{
    // Methods 
    IAppLogger CreateLogger<TCategoryType>();
    IAppLogger CreateLogger(Type categoryNameType);
}