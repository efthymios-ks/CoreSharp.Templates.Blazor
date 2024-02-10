using System;
using System.Collections.Generic;

namespace WebClient.Services.Interfaces;

public interface IUserContext
{
    // Properties
    bool IsAuthenticated { get; }
    string Id { get; }
    string FullName { get; }
    string Username { get; }
    string Email { get; }
    string PhotoUrl { get; }
    IReadOnlyDictionary<string, string> Claims { get; }

    // Events
    event Action Changed;
}
