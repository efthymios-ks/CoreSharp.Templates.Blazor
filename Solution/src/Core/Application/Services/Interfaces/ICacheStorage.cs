using System;
using System.Threading.Tasks;

namespace CoreSharp.Templates.Blazor.Application.Services.Interfaces;

public interface ICacheStorage
{
    // Methods
    Task SetAsync<TData>(string key, TData data, TimeSpan duration);
    Task<(bool found, TData data)> GetAsync<TData>(string key);
}
