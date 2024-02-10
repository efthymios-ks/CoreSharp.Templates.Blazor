using Microsoft.AspNetCore.Builder;

namespace WebApi.Extensions;

/// <summary>
/// <see cref="WebApplication"/> extensions.
/// </summary>
internal static class WebApplicationExtensions
{
    public static WebApplication ConfigureAppPipeline(this WebApplication app)
    {
        var configuration = app.Configuration;
        var environment = app.Environment;
        app.UseAppApi(configuration, environment);

        return app;
    }
}