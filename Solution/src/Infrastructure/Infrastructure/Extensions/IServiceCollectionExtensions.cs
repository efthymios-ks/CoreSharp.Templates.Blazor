using CoreSharp.Templates.Blazor.Application.Extensions;
using CoreSharp.Templates.Blazor.Application.Services.Interfaces;
using CoreSharp.Templates.Blazor.Application.Services.Interfaces.Localization;
using CoreSharp.Templates.Blazor.Application.Services.Interfaces.Logging;
using CoreSharp.Templates.Blazor.Domain.Repositories;
using CoreSharp.Templates.Blazor.Infrastructure.Configurations;
using CoreSharp.Templates.Blazor.Infrastructure.Data;
using CoreSharp.Templates.Blazor.Infrastructure.Services;
using CoreSharp.Templates.Blazor.Infrastructure.Services.Localization;
using CoreSharp.Templates.Blazor.Infrastructure.Services.Logging;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace CoreSharp.Templates.Blazor.Infrastructure.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(serviceCollection);
        ArgumentNullException.ThrowIfNull(configuration);

        serviceCollection.AddAppCurrentIdentityService();
        serviceCollection.AddAppDbContext(configuration);
        serviceCollection.AddAppUnitOfWork();
        serviceCollection.AddAppLogger();
        serviceCollection.AddAppConfigurations();
        serviceCollection.AddAppLocalization();

        return serviceCollection;
    }

    private static IServiceCollection AddAppCurrentIdentityService(this IServiceCollection serviceCollection)
        => serviceCollection.AddScoped<ICurrentIdentityService, CurrentIdentityService>();

    private static IServiceCollection AddAppDbContext(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection.AddDbContextPool<AppDbContext>(DbContextConfigure);

        void DbContextConfigure(DbContextOptionsBuilder options)
            => options.UseSqlServer(GetConnectionString(), SqlServerConfigure);

        string GetConnectionString()
        {
            var connectionStrings = configuration
                .GetSection(ConnectionStringsOptions.SectionName)
                .Get<ConnectionStringsOptions>();

            return configuration.GetConnectionString(connectionStrings.Database);
        }

        static void SqlServerConfigure(SqlServerDbContextOptionsBuilder options)
              => options.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
    }

    private static IServiceCollection AddAppUnitOfWork(this IServiceCollection serviceCollection)
        => serviceCollection.AddScoped<IAppUnitOfWork, AppUnitOfWork>();

    private static IServiceCollection AddAppLogger(this IServiceCollection serviceCollection)
        => serviceCollection.AddMicrosoftAppLogger();

    private static IServiceCollection AddMicrosoftAppLogger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });

        serviceCollection.AddTransient(typeof(IAppLogger<>), typeof(MicrosoftAppLogger<>));
        serviceCollection.AddSingleton<IAppLoggerFactory, MicrosoftAppLoggerFactory>();

        return serviceCollection;
    }

    private static IServiceCollection AddAppConfigurations(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        serviceCollection.AddOptions<ConnectionStringsOptions>()
                         .BindConfiguration(ConnectionStringsOptions.SectionName)
                         .ValidateFluently()
                         .ValidateOnStart();

        return serviceCollection;
    }

    private static IServiceCollection AddAppLocalization(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMemoryCache();
        serviceCollection.AddScoped(typeof(IAppStringLocalizer<>), typeof(JsonAppStringLocalizer<>));
        serviceCollection.AddSingleton<IAppStringLocalizerFactory, JsonAppStringLocalizerFactory>();

        return serviceCollection;
    }
}