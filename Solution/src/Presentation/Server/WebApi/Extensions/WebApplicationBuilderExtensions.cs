using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace WebApi.Extensions;

/// <summary>
/// <see cref="WebApplicationBuilder"/> extensions.
/// </summary>
internal static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureAppLogging(this WebApplicationBuilder builder)
    {
        var logging = builder.Logging;
        var configuration = builder.Configuration;

        logging.ClearProviders();
        logging.AddConfiguration(configuration.GetSection("Logging"));
        logging.AddConsole();
        logging.AddDebug();

        return builder;
    }

    public static WebApplicationBuilder ConfigureAppServices(this WebApplicationBuilder builder)
    {
        var serviceCollection = builder.Services;
        var configuration = builder.Configuration;
        serviceCollection.AddAppApi(configuration);

        return builder;
    }
}
