using System;

namespace WebClient.Services.Interfaces.Localization;

public interface IAppStringLocalizerFactory
{
    // Methods
    IAppStringLocalizer Create(Type resourceType);
}