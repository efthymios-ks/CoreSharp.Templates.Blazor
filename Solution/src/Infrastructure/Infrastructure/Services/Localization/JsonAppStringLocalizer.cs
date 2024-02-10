using CoreSharp.Templates.Blazor.Application.Services.Interfaces.Localization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreSharp.Templates.Blazor.Infrastructure.Services.Localization;

internal sealed class JsonAppStringLocalizer : IAppStringLocalizer
{
    // Fields 
    private readonly string _fileName;
    private readonly ConcurrentDictionary<string, string> _source = new();

    // Constructors 
    public JsonAppStringLocalizer(string fileName)
    {
        ArgumentException.ThrowIfNullOrEmpty(fileName);

        _fileName = fileName;
    }

    // Properties 
    public string this[string key]
        => this[key, Array.Empty<object>()];

    public string this[string key, object[] arguments]
       => GetLocalizedString(key, arguments);

    // Methods 
    private string GetLocalizedString(string key, params object[] arguments)
    {
        TryCacheDictionaryAsync()
            .GetAwaiter()
            .GetResult();

        return _source.TryGetValue(key, out var value)
            ? string.Format(value, arguments)
            : key;
    }

    private async Task TryCacheDictionaryAsync()
    {
        if (!_source.IsEmpty)
        {
            return;
        }

        // Get file 
        using var fileStream = new FileStream(_fileName, FileMode.Open, FileAccess.Read);

        // Deserialize file into dictionary 
        var dictionary = await JsonSerializer.DeserializeAsync<Dictionary<string, string>>(fileStream);

        // Update dictionary 
        foreach (var pair in dictionary)
        {
            _source.AddOrUpdate(pair.Key, pair.Value, (_, __) => pair.Value);
        }
    }
}
