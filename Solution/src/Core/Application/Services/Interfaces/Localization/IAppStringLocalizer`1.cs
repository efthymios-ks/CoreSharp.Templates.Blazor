namespace CoreSharp.Templates.Blazor.Application.Services.Interfaces.Localization;

public interface IAppStringLocalizer<out TResource> : IAppStringLocalizer
    where TResource : class
{
}
