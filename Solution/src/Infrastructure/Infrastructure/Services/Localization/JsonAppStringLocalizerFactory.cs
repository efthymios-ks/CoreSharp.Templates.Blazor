﻿using CoreSharp.Templates.Blazor.Application.Services.Interfaces;
using CoreSharp.Templates.Blazor.Application.Services.Interfaces.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSharp.Templates.Blazor.Infrastructure.Services.Localization;

public sealed class JsonAppStringLocalizerFactory : IAppStringLocalizerFactory
{
    // Fields 
    private static readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(15);
    private readonly ICacheStorage _cacheStorage;

    // Constructors 
    public JsonAppStringLocalizerFactory(ICacheStorage cacheStorage)
        => _cacheStorage = cacheStorage;

    // Properties 
    private static string ContentPath
        => AppContext.BaseDirectory;

    private static char EnvironmentPathDelimiter
        => Path.DirectorySeparatorChar;

    // Methods 
    public IAppStringLocalizer Create(Type resourceType)
    {
        // Get lookup paths 
        var fileName = GetNormalizedResourceFileName(resourceType);
        var lookupFiles = GetLookupPaths(fileName, CultureInfo.CurrentCulture)
            .Select(path => $"{path}.json");

        // Check results if any file found 
        var resourceFilePath = lookupFiles.FirstOrDefault(File.Exists)
            ?? throw new FileNotFoundException($"No resource file found for '{resourceType.FullName}'.");

        return GetCachedLocalizerAsync(resourceFilePath)
            .GetAwaiter()
            .GetResult();
    }

    private async Task<IAppStringLocalizer> GetCachedLocalizerAsync(string fileName)
    {
        var localizerCacheKey = GetLocalizerCacheKey(fileName);
        var (found, localizer) = await _cacheStorage.GetAsync<IAppStringLocalizer>(localizerCacheKey);

        if (!found)
        {
            localizer = new JsonAppStringLocalizer(fileName);
            await _cacheStorage.SetAsync(localizerCacheKey, localizer, _cacheDuration);
        }

        return localizer;
    }

    private static IEnumerable<string> GetLookupPaths(string fileName, CultureInfo cultureInfo)
    {
        // Setup languages 
        var languages = new HashSet<string>
        {
            // en-US 
            cultureInfo.Name,
            // en (fallback) 
            cultureInfo.TwoLetterISOLanguageName,
            // No language (fallback) 
            string.Empty
        };

        // Build paths 
        var lookupPaths = new HashSet<string>();
        foreach (var language in languages)
        {
            lookupPaths.Add(BuildLookupFilePath(fileName, language));
        }

        return lookupPaths;
    }

    private static string BuildLookupFilePath(string fileName, string language)
    {
        var builder = new StringBuilder();

        builder.Append(fileName);

        if (!string.IsNullOrWhiteSpace(language))
        {
            builder.Append('.').Append(language);
        }

        return builder.ToString();
    }

    private static string GetNormalizedResourceFileName(Type resourceType)
    {
        var location = resourceType.Assembly.GetName().Name;
        var fileName = resourceType.FullName;
        var commonPart = FindCommonSuffixPrefix(location, fileName);
        if (commonPart.Length > 0)
        {
            fileName = fileName[(commonPart.Length + 1)..];
        }

        fileName = fileName.Replace('.', EnvironmentPathDelimiter);
        return Path.Combine(ContentPath, fileName);
    }

    /// <summary>
    /// Find common part between left string ending (suffix) and right string starting (prefix).
    /// <code>
    /// // CommonPart = "WebApp"
    /// var commonpart = FindCommonSuffixPrefix("MyApp.WebApp", "WebApp.Features.Home.HomePage");
    /// </code>
    /// </summary>
    private static string FindCommonSuffixPrefix(string left, string right)
    {
        var minLength = Math.Min(left.Length, right.Length);
        var commonLength = 0;

        for (var i = 1; i <= minLength; i++)
        {
            var leftSubstring = left[^i..];
            var rightSubstring = right[..i];

            if (leftSubstring == rightSubstring)
            {
                commonLength = i;
            }
        }

        return left[^commonLength..];
    }

    private static string GetLocalizerCacheKey(string fileName)
        => $"{nameof(JsonAppStringLocalizerFactory)}_{fileName}";
}