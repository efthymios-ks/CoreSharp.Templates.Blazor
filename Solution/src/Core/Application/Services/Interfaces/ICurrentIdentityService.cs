using System.Collections.Generic;

namespace CoreSharp.Templates.Blazor.Application.Services.Interfaces;

public interface ICurrentIdentityService
{
    // Properties
    bool IsAuthenticated { get; }
    string Id { get; }
    string FullName { get; }
    string Username { get; }
    string Email { get; }
    IReadOnlyDictionary<string, string> Claims { get; }
}