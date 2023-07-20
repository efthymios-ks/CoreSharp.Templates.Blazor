namespace CoreSharp.Templates.Blazor.Application.Services.Interfaces.Localization;

public interface IAppStringLocalizer
{
    // Properties
    string this[string key] { get; }
    string this[string key, object[] arguments] { get; }
}
