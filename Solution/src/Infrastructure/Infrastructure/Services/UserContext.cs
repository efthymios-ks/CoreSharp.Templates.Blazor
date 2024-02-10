using CoreSharp.Extensions;
using CoreSharp.Templates.Blazor.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;

namespace CoreSharp.Templates.Blazor.Infrastructure.Services;

internal sealed class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private bool? _isAuthenticated;
    private string _id;
    private string _fullName;
    private string _username;
    private string _email;
    private string _photoUrl;
    private IReadOnlyDictionary<string, string> _claims;

    // Constructors
    public UserContext(IHttpContextAccessor httpContextAccessor)
        => _httpContextAccessor = httpContextAccessor;

    // Properties
    public bool IsAuthenticated
        => _isAuthenticated ??= GetIsAuthenticated();

    public string Id
        => _id ??= GetId();

    public string FullName
        => _fullName ??= GetFullName();

    public string Username
        => _username ??= GetUsername();

    public string Email
        => _email ??= GetEmail();

    public string PhotoUrl
        => _photoUrl ??= GetPhotoUrl();

    public IReadOnlyDictionary<string, string> Claims
        => _claims ??= GetClaims();

    private ClaimsPrincipal ClaimsPrincipal
        => _httpContextAccessor.HttpContext.User;

    // Methods  
    private bool GetIsAuthenticated()
        => ClaimsPrincipal?.Identity?.IsAuthenticated is true;

    private string GetId()
        => GetClaimValue(ClaimTypes.NameIdentifier);

    private string GetFullName()
        => GetClaimValue("name");

    private string GetUsername()
        => GetClaimValue("nickname");

    private string GetEmail()
        => GetClaimValue(ClaimTypes.Email);

    private string GetPhotoUrl()
        => GetClaimValue("picture");

    private string GetClaimValue(string claimType)
        => ClaimsPrincipal?.FindFirst(claimType)?.Value;

    private IReadOnlyDictionary<string, string> GetClaims()
    {
        var dictionary = ClaimsPrincipal?.Claims?
            .ToDictionary(c => c.Type, c => c.Value) ?? new();
        return new ReadOnlyDictionary<string, string>(dictionary);
    }
}