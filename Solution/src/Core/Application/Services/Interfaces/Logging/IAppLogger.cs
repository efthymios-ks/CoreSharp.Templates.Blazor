using CoreSharp.Templates.Blazor.Application.Enums;
using System;
using System.Diagnostics.CodeAnalysis;

namespace CoreSharp.Templates.Blazor.Application.Services.Interfaces.Logging;

[SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "<Pending>")]
public interface IAppLogger
{
    // Methods 
    void Debug(string message, params object[] args);
    void Info(string message, params object[] args);
    void Warning(string message, params object[] args);
    void Error(string message, params object[] args);
    void Error(Exception exception, string message, params object[] args);
    void IsEnabled(AppLogLevel level);
}
