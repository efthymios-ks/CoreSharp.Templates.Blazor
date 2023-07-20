namespace CoreSharp.Templates.Blazor.Infrastructure.Configurations;

public sealed class ConnectionStringsOptions
{
    // Fields 
    public const string SectionName = "ConnectionStrings";

    // Properties 
    public string Database { get; init; }
}
