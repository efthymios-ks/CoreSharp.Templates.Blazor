using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using WebClient.Services.Interfaces;

namespace WebClient.Services;

public sealed class InMemoryCacheStorage : ICacheStorage
{
    // Fields
    private readonly IMemoryCache _memoryCache;

    // Constructors
    public InMemoryCacheStorage(IMemoryCache memoryCache)
        => _memoryCache = memoryCache;

    public Task SetAsync<TData>(string key, TData data, TimeSpan duration)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        _memoryCache.Set(key, data, duration);
        return Task.CompletedTask;
    }

    public Task<(bool found, TData data)> GetAsync<TData>(string key)
    {
        if (_memoryCache.TryGetValue(key, out TData data))
        {
            return ToTask(true, data);
        }

        return ToTask(false, default);

        static Task<(bool, TData)> ToTask(bool found, TData data)
             => Task.FromResult((found, data));
    }
}
