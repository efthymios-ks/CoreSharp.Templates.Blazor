using CoreSharp.Blazor.Components.Abstracts;
using CoreSharp.Templates.Blazor.Application.Services.Interfaces;
using CoreSharp.Templates.Blazor.Application.Services.Interfaces.Localization;
using CoreSharp.Templates.Blazor.Application.Services.Interfaces.Logging;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace WebApp.Features.Abstracts;

public abstract class AppComponentBase : RazorComponentBase
{
    // Fields
    private IAppLogger _appLogger;
    private IAppStringLocalizer _appStringLocalizer;

    // Properties
    [Inject]
    protected IMediator Mediator { get; set; }

    [Inject]
    protected ICurrentIdentityService CurrentIdentityService { get; set; }

    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    [Inject]
    private IAppLoggerFactory AppLoggerFactory { get; set; }

    [Inject]
    private IAppStringLocalizerFactory AppStringLocalizerFactory { get; set; }

    protected IAppLogger Logger
        => _appLogger ??= AppLoggerFactory.CreateLogger(GetType());

    protected IAppStringLocalizer StringLocalizer
        => _appStringLocalizer ??= AppStringLocalizerFactory.Create(GetType());
}
