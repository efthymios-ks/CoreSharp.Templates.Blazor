using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;

namespace WebClient.Extensions;

/// <summary>
/// <see cref="WebAssemblyHostBuilder"/> extensions.
/// </summary>
internal static class WebAssemblyHostBuilderExtensions
{
    public static WebAssemblyHostBuilder ConfigureServices(this WebAssemblyHostBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var serviceCollection = builder.Services;
        var configuration = builder.Configuration;
        serviceCollection.AddAppClient(configuration);

        return builder;
    }

    public static WebAssemblyHostBuilder ConfigureLogging(this WebAssemblyHostBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var configuration = builder.Configuration;
        var loggingBuilder = builder.Logging;
        loggingBuilder.ConfigureAppLogging(configuration);

        return builder;
    }
}
