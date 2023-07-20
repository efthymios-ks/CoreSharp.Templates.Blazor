using System;

namespace CoreSharp.Templates.Blazor.Application.Services.Interfaces.Localization;

public interface IAppStringLocalizerFactory
{
    // Methods
    IAppStringLocalizer Create(Type resourceType);
}