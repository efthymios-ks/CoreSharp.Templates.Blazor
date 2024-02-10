using CoreSharp.AspNetCore.Extensions;
using CoreSharp.Templates.Blazor.Application.Extensions;
using CoreSharp.Templates.Blazor.Infrastructure.Configurations;
using CoreSharp.Templates.Blazor.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using WebApi.Constants;
using WebApi.Swagger.ConfigureNamedOptions;
using WebApi.Swagger.OperationFilters;

namespace WebApi.Extensions;

/// <summary>
/// <see cref="IServiceCollection"/> extensions.
/// </summary>
internal static class IServiceCollectionExtensions
{
    public static IServiceCollection AddAppApi(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        // System 
        serviceCollection.AddHttpContextAccessor();
        serviceCollection.AddAppControllers();
        serviceCollection.AddEndpointsApiExplorer();

        // User 
        serviceCollection.AddApplication();
        serviceCollection.AddInfrastructure(configuration);
        serviceCollection.AddAppCors(configuration);
        serviceCollection.AddAppApiVersioning();
        serviceCollection.AddAppSwagger(configuration);

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

    private static IServiceCollection AddAppSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        var auth0Options = configuration
           .GetSection(Auth0Options.SectionName)
           .Get<Auth0Options>();

        services.AddSwaggerGen(config =>
        {
            // Authentication 
            const string oauth2Key = "oauth2";
            var authority = auth0Options.Authority.TrimEnd('/');
            var audience = auth0Options.Audience;
            config.AddSecurityDefinition(oauth2Key, new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                BearerFormat = JwtConstants.HeaderType,
                Flows = new()
                {
                    Implicit = new()
                    {
                        TokenUrl = new($"{authority}/oauth/token"),
                        AuthorizationUrl = new Uri($"{authority}/authorize?audience={audience}"),
                        Scopes = auth0Options.Scopes.ToDictionary(scope => scope, scope => scope)
                    }
                }
            });

            config.AddSecurityRequirement(new()
            {
                {
                    new ()
                    {
                        Reference = new ()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = oauth2Key
                        }
                    },
                    new[] { "openid" }
                }
            });

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
