using CoreSharp.AspNetCore.Middlewares.Abstracts;
using CoreSharp.Templates.Blazor.Application.Services.Interfaces.Logging;
using Microsoft.AspNetCore.Http;

namespace WebApp.Middlewares;

internal sealed class AppRequestLogMiddleware : RequestLogMiddlewareBase
{
    // Fields
    private readonly IAppLogger _logger;

    // Constructors
    public AppRequestLogMiddleware(
        RequestDelegate next,
        IAppLogger<AppRequestLogMiddleware> logger)
        : base(next)
        => _logger = logger;

    protected override void Log(string logEntry)
        => _logger.Info(logEntry);
}
