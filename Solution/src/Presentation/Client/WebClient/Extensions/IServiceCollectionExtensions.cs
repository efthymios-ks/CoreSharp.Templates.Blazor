using CoreSharp.Blazor.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using WebClient.Services;
using WebClient.Services.Interfaces;
using WebClient.Services.Interfaces.Localization;
using WebClient.Services.Interfaces.Logging;
using WebClient.Services.Localization;
using WebClient.Services.Logging;

namespace WebClient.Extensions;

/// <summary>
/// <see cref="IServiceCollection"/> extensions.
/// </summary>
internal static partial class IServiceCollectionExtensions
{
    public static IServiceCollection AddAppClient(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(serviceCollection);

        // Local 
        serviceCollection.AddAppUserContext();
        serviceCollection.AddAppMediator();
        serviceCollection.AddAppAuthentication(configuration);
        serviceCollection.AddApiClient(configuration);
        serviceCollection.AddAppCacheStorage();
        serviceCollection.AddAppLocalization();
        serviceCollection.AddAppLogger();
        serviceCollection.AddAppLocalization();
        serviceCollection.AddCoreSharpBlazor();
        serviceCollection.AddScoped<IErrorHandlingService, ErrorHandlingService>();

        return serviceCollection;
    }

    private static IServiceCollection AddAppUserContext(this IServiceCollection serviceCollection)
        => serviceCollection.AddScoped<IUserContext, UserContext>();

    private static IServiceCollection AddAppMediator(this IServiceCollection serviceCollection)
        => serviceCollection.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetEntryAssembly()));

    private static IServiceCollection AddAppAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddOidcAuthentication(options => configuration.Bind("Authentication:Auth0", options.ProviderOptions));

        return serviceCollection;
    }

    private static IServiceCollection AddAppCacheStorage(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddMemoryCache()
            .AddSingleton<ICacheStorage, InMemoryCacheStorage>();

    private static IServiceCollection AddAppLocalization(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddLocalization(options =>
            {
                options.ResourcesPath = "Localization";
            })
            .AddScoped(typeof(IAppStringLocalizer<>), typeof(JsonAppStringLocalizer<>))
            .AddSingleton<IAppStringLocalizerFactory, JsonAppStringLocalizerFactory>();

    private static IServiceCollection AddApiClient(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection
            .AddScoped<IApiClient, ApiClient>();

        serviceCollection
            .AddHttpClient<IApiClient, ApiClient>(client =>
            {
                var apiEndpoint = configuration["Endpoints:Api"]?.TrimEnd('/') + '/';
                client.BaseAddress = new(apiEndpoint);
            })
            .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

        return serviceCollection;
    }

    private static IServiceCollection AddAppLogger(this IServiceCollection serviceCollection)
        => serviceCollection.AddMicrosoftAppLogger();

    private static IServiceCollection AddMicrosoftAppLogger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient(typeof(IAppLogger<>), typeof(MicrosoftAppLogger<>));
        serviceCollection.AddSingleton<IAppLoggerFactory, MicrosoftAppLoggerFactory>();

        return serviceCollection;
    }
}
