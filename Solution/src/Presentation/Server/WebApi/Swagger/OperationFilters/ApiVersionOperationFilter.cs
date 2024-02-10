using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using WebApi.Constants;

namespace WebApi.Swagger.OperationFilters;

internal sealed class ApiVersionOperationFilter : IOperationFilter
{
    // Methods 
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        RemoveApiVersionQueryParameter(operation);
        SetApiVersionHeaderDefaultValue(operation, context);
    }

    private static void RemoveApiVersionQueryParameter(OpenApiOperation operation)
    {
        var versionParameters = operation.Parameters.Where(p => p.Name == ApiVersioning.QueryParameterKey)
                                                    .ToArray();
        foreach (var versionParameter in versionParameters)
        {
            operation.Parameters.Remove(versionParameter);
        }
    }

    private static void SetApiVersionHeaderDefaultValue(OpenApiOperation operation, OperationFilterContext context)
    {
        var apiVersionParameter = GetApiVersionHeaderParameter(operation);
        if (apiVersionParameter is null)
        {
            return;
        }

        var apiVersionValue = GetHeaderApiVersionValue(context);
        if (string.IsNullOrEmpty(apiVersionValue))
        {
            return;
        }

        apiVersionParameter.Required = false;
        apiVersionParameter.Example = new OpenApiString(apiVersionValue);
    }

    private static OpenApiParameter GetApiVersionHeaderParameter(OpenApiOperation operation)
        => operation.Parameters.FirstOrDefault(p => p.In == ParameterLocation.Header && IsApiVersionHeader(p.Name));

    private static string GetHeaderApiVersionValue(OperationFilterContext context)
    {
        var apiVersion = context.ApiDescription.ParameterDescriptions
            .FirstOrDefault(parameter => IsApiVersionHeader(parameter.Name));

        return $"{apiVersion?.DefaultValue}";
    }

    private static bool IsApiVersionHeader(string parameterName)
        => string.Equals(parameterName, ApiVersioning.HeaderKey, StringComparison.OrdinalIgnoreCase);
}