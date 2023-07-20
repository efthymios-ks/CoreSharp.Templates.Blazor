using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace WebApp.Swagger.ConfigureNamedOptions;

internal sealed class SwaggerGenConfigureNamedOptions : IConfigureNamedOptions<SwaggerGenOptions>
{
    // Fields
    private readonly IApiVersionDescriptionProvider _apiVersionProvider;

    // Constructors
    public SwaggerGenConfigureNamedOptions(IApiVersionDescriptionProvider apiProvider)
        => _apiVersionProvider = apiProvider;

    // Methods
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _apiVersionProvider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
        }
    }

    public void Configure(string name, SwaggerGenOptions options)
        => Configure(options);

    private static OpenApiInfo CreateVersionInfo(ApiVersionDescription apiVersionDescription)
        => new()
        {
            Title = GetApiTitle(apiVersionDescription),
            Version = $"{apiVersionDescription.ApiVersion}"
        };

    private static string GetApiTitle(ApiVersionDescription apiVersionDescription)
    {
        var currentAssembly = Assembly.GetEntryAssembly().GetName();
        var title = currentAssembly.Name;

        if (apiVersionDescription.IsDeprecated)
        {
            title += " [Deprecated]";
        }

        return title;
    }
}
