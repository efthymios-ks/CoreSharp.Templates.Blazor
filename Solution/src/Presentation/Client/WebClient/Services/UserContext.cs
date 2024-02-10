using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebClient.Services.Interfaces;

namespace WebClient.Services;

public sealed class UserContext : IUserContext, IDisposable
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private bool _isDisposed;

    // Constructors
    public UserContext(AuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
        _authenticationStateProvider.AuthenticationStateChanged += AuthenticationStateChangedCallback;
    }

    // Properties
    public bool IsAuthenticated { get; private set; }
    public string Id
        => Email;
    public string FullName { get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string PhotoUrl { get; private set; }
    public IReadOnlyDictionary<string, string> Claims { get; private set; }

    // Events  
    public event Action Changed;

    // Methods 
    private void AuthenticationStateChangedCallback(Task<AuthenticationState> task)
    {
        var authenticationState = task.Result;
        var claimsPrincipal = authenticationState.User;
        IsAuthenticated = GetIsAuthenticated(claimsPrincipal);

        if (IsAuthenticated)
        {
            SetIdentityProperties(claimsPrincipal);
        }
        else
        {
            ClearIdentityProperties();
        }

        OnChanged();
    }

    private void ClearIdentityProperties()
    {
        FullName = null;
        Username = null;
        Email = null;
        PhotoUrl = null;
        Claims = new Dictionary<string, string>();
    }

    private void SetIdentityProperties(ClaimsPrincipal claimsPrincipal)
    {
        FullName = GetFullName(claimsPrincipal);
        Username = GetUsername(claimsPrincipal);
        Email = GetEmail(claimsPrincipal);
        PhotoUrl = GetPhotoUrl(claimsPrincipal);
        Claims = GetClaims(claimsPrincipal);
    }

    private static bool GetIsAuthenticated(ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal?.Identity?.IsAuthenticated is true;

    private static string GetFullName(ClaimsPrincipal claimsPrincipal)
        => GetClaimValue(claimsPrincipal, "name");

    private static string GetUsername(ClaimsPrincipal claimsPrincipal)
        => GetClaimValue(claimsPrincipal, "nickname");

    private static string GetEmail(ClaimsPrincipal claimsPrincipal)
        => GetClaimValue(claimsPrincipal, "email");

    private static string GetPhotoUrl(ClaimsPrincipal claimsPrincipal)
        => GetClaimValue(claimsPrincipal, "picture");

    private static string GetClaimValue(ClaimsPrincipal claimsPrincipal, string claimType)
        => claimsPrincipal?.FindFirst(claimType)?.Value;

    private static IReadOnlyDictionary<string, string> GetClaims(ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal?.Claims?.ToDictionary(claim => claim.Type, c => c.Value) ?? new();

    private void OnChanged()
        => Changed?.Invoke();

    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        _isDisposed = true;
        _authenticationStateProvider.AuthenticationStateChanged -= AuthenticationStateChangedCallback;
    }
}
