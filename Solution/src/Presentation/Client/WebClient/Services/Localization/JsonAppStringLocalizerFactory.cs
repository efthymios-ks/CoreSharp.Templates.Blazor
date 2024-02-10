using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClient.Services.Interfaces;
using WebClient.Services.Interfaces.Localization;

namespace WebClient.Services.Localization;

// TODO: Implement with EmbeddedFiles.
public sealed class JsonAppStringLocalizerFactory : IAppStringLocalizerFactory
{
    // Fields 
    private static readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(15);
    private readonly string _resourcesPath;
    private readonly ICacheStorage _cacheStorage;

    // Constructors 
    public JsonAppStringLocalizerFactory(
        IOptions<LocalizationOptions> options,
        ICacheStorage cacheStorage)
    {
        _resourcesPath = options.Value.ResourcesPath;
        _cacheStorage = cacheStorage;
    }

    // Methods 
    public IAppStringLocalizer Create(Type resourceType)
    {
        // Get lookup paths 
        var fileName = GetNormalizedResourceFileName(resourceType);
        var lookupFiles = GetLookupPaths(fileName, CultureInfo.CurrentCulture)
            .Select(path => $"{path}.json");

        // Check results if any file found
        var fileProvider = new EmbeddedFileProvider(resourceType.Assembly, baseNamespace: null);
        var files = lookupFiles.Select(fileProvider.GetFileInfo);
        var fileInfo = files.FirstOrDefault(file => file.Exists)
            ?? throw new FileNotFoundException($"No resource file found for '{resourceType.FullName}'.");

        return GetCachedLocalizerAsync(fileInfo)
            .GetAwaiter()
            .GetResult();
    }

    private async Task<IAppStringLocalizer> GetCachedLocalizerAsync(IFileInfo fileInfo)
    {
        var localizerCacheKey = GetLocalizerCacheKey(fileInfo);
        var (found, localizer) = await _cacheStorage.GetAsync<IAppStringLocalizer>(localizerCacheKey);

        if (!found)
        {
            localizer = new JsonAppStringLocalizer(fileInfo);
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

    private string GetNormalizedResourceFileName(Type resourceType)
    {
        var location = resourceType.Assembly.GetName().Name;
        var fileName = resourceType.FullName;
        if (!string.IsNullOrWhiteSpace(_resourcesPath))
        {
            var commonPart = FindCommonSuffixPrefix(location, fileName);
            if (commonPart.Length > 0)
            {
                var commonPartIndex = fileName.IndexOf(commonPart, StringComparison.Ordinal);
                var resourcesPathSegment = $".{_resourcesPath}";
                fileName = fileName.Insert(commonPartIndex + commonPart.Length, resourcesPathSegment);
            }
        }

        return fileName;
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

    private static string GetLocalizerCacheKey(IFileInfo fileInfo)
        => $"{nameof(JsonAppStringLocalizerFactory)}_{fileInfo.PhysicalPath}";
}