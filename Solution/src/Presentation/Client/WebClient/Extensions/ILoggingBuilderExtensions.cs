using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebClient.Extensions;

internal static class ILoggingBuilderExtensions
{
    public static void ConfigureAppLogging(this ILoggingBuilder loggingBuilder, IConfiguration configuration)
    {
        var loggingConfiguration = configuration.GetSection("Logging");
        loggingBuilder.AddConfiguration(loggingConfiguration);
        loggingBuilder.AddDebug();
    }
}