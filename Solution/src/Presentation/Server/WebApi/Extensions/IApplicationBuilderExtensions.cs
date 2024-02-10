using CoreSharp.Templates.Blazor.Infrastructure.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using WebApi.Middlewares;

namespace WebApi.Extensions;

/// <summary>
/// <see cref="IApplicationBuilder"/> extensions.
/// </summary>
internal static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder UseAppApi(
        this IApplicationBuilder app,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(app);

        // Environment dependent
        if (environment.IsDevelopment())
        {
            var apiVersionProvider = GetService<IApiVersionDescriptionProvider>();
            app.UseAppSwagger(configuration, apiVersionProvider);
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }

        // System 
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAppCors();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => endpoints.MapControllers());

        // User 
        app.UseMiddleware<AppRequestLogMiddleware>();

        return app;

        TService GetService<TService>()
            => app.ApplicationServices.GetRequiredService<TService>();
    }

    private static IApplicationBuilder UseAppCors(this IApplicationBuilder app)
        => app.UseCors();

    private static IApplicationBuilder UseAppSwagger(
        this IApplicationBuilder app,
        IConfiguration configuration,
        IApiVersionDescriptionProvider apiVersionProvider)
    {
        var auth0Options = configuration
           .GetSection(Auth0Options.SectionName)
           .Get<Auth0Options>();

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

            // Authentication 
            config.OAuthClientId(auth0Options.ClientId);
            config.OAuthClientSecret(auth0Options.ClientSecret);
            config.OAuthUsePkce();
        });

        return app;
    }
}
