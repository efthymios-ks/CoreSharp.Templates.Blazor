using System;

namespace CoreSharp.Templates.Blazor.Infrastructure.Configurations;

public sealed class Auth0Options
{
    // Fields 
    public const string SectionName = "Authentication:Auth0";

    // Properties 
    public string Authority { get; set; }
    public string Audience { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string[] Scopes { get; set; } = Array.Empty<string>();
}
