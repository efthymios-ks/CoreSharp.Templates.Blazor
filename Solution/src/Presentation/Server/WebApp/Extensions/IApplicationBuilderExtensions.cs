using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Linq;

namespace WebApp.Extensions;

/// <summary>
/// <see cref="IApplicationBuilder"/> extensions.
/// </summary>
internal static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder UseAppCors(this IApplicationBuilder app)
    {
        app.UseCors();

        return app;
    }

    public static IApplicationBuilder UseAppSwagger(
        this IApplicationBuilder app,
        IApiVersionDescriptionProvider apiVersionProvider)
    {
        app.UseSwagger();
        app.UseSwaggerUI(config =>
        {
            // Display newest first. 
            var versionGroupNames = apiVersionProvider.ApiVersionDescriptions
                .Select(d => d.GroupName)
                .Reverse();

            foreach (var versionGroupName in versionGroupNames)
            {
                config.SwaggerEndpoint(
                    $"/swagger/{versionGroupName}/swagger.json",
                    versionGroupName.ToUpperInvariant());
            }
        });

        return app;
    }
}
