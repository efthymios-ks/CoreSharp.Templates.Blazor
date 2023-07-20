using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApp.Extensions;

/// <summary>
/// <see cref="IWebHostBuilder"/> extensions.
/// </summary>
internal static class IWebHostBuilderExtensions
{
    public static IWebHostBuilder ConfigureAppLogging(this IWebHostBuilder builder)
        => builder.ConfigureLogging((context, logging) =>
        {
            logging.ClearProviders();
            logging.AddConfiguration(context.Configuration.GetSection("Logging"));
            logging.AddConsole();
            logging.AddDebug();
        });
}
