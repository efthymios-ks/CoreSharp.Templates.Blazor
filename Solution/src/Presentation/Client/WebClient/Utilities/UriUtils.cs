using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;

namespace WebClient.Utilities;

internal static class UriUtils
{
    public static bool TryAppendRelativeToCurrentBaseUrl(IWebAssemblyHostEnvironment environment, string relativePath, out string absolutePath)
    {
        if (Uri.IsWellFormedUriString(relativePath, UriKind.Relative))
        {
            absolutePath = CombineUrls(environment.BaseAddress, relativePath);
            return true;
        }

        absolutePath = null;
        return false;
    }

    private static string CombineUrls(string leftUrl, string rightUrl)
    {
        leftUrl = leftUrl?.TrimEnd('/') ?? string.Empty;
        rightUrl = rightUrl?.TrimStart('/') ?? string.Empty;
        return $"{leftUrl}/{rightUrl}";
    }
}