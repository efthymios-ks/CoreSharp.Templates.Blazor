using CoreSharp.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using WebApp.Constants;
using WebApp.Swagger.ConfigureNamedOptions;
using WebApp.Swagger.OperationFilters;

namespace WebApp.Extensions;

/// <summary>
/// <see cref="IServiceCollection"/> extensions.
/// </summary>
internal static class IServiceCollectionExtensions
{
    public static IServiceCollection AddAppBlazor(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddRazorPages(options => options.RootDirectory = "/Features");
        serviceCollection.AddServerSideBlazor();

        return serviceCollection;
    }

    public static IServiceCollection AddAppApi(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        // System 
        serviceCollection.AddHttpContextAccessor();
        serviceCollection.AddAppControllers();
        serviceCollection.AddEndpointsApiExplorer();

        // User 
        serviceCollection.AddAppCors(configuration);
        serviceCollection.AddAppApiVersioning();
        serviceCollection.AddAppSwagger();

        return serviceCollection;
    }

    private static IServiceCollection AddAppControllers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddControllers();

        return serviceCollection;
    }

    private static IServiceCollection AddAppApiVersioning(this IServiceCollection serviceCollection)
    {
        // Needed for Swagger integration 
        serviceCollection.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        serviceCollection.AddApiVersioning(options =>
        {
            var apiVersionReader = ApiVersionReader.Combine(
                new HeaderApiVersionReader { HeaderNames = { ApiVersioning.HeaderKey } },
                new QueryStringApiVersionReader(ApiVersioning.QueryParameterKey)
            );

            options.ReportApiVersions = true;
            options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            options.ApiVersionReader = apiVersionReader;
            options.RouteConstraintName = "apiVersion";
            options.AssumeDefaultVersionWhenUnspecified = true;
        });

        return serviceCollection;
    }

    private static IServiceCollection AddAppCors(this IServiceCollection serviceCollection, IConfiguration configuration)
        => serviceCollection.AddCors(configuration);

    private static IServiceCollection AddAppSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(config =>
        {
            // Add xml documentation 
            var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
            config.IncludeXmlComments(xmlFilePath);

            // Add api version filter
            config.OperationFilter<ApiVersionOperationFilter>();
        });

        services.ConfigureOptions<SwaggerGenConfigureNamedOptions>();

        return services;
    }
}
