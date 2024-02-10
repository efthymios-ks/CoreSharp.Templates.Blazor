﻿using System;
using WebClient.Services.Interfaces.Localization;

namespace WebClient.Services.Localization;

internal sealed class JsonAppStringLocalizer<TResource> : IAppStringLocalizer<TResource>
    where TResource : class
{
    // Fields
    private readonly IAppStringLocalizer _appStringLocalizer;

    // Constructors
    public JsonAppStringLocalizer(IAppStringLocalizerFactory appStringLocalizerFactory)
        => _appStringLocalizer = appStringLocalizerFactory.Create(typeof(TResource));

    // Methods 
    public string this[string key]
        => this[key, Array.Empty<object>()];

    public string this[string key, object[] arguments]
        => _appStringLocalizer[key, arguments];
}