using CoreSharp.Blazor.Components.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Components;
using WebClient.Services.Interfaces;
using WebClient.Services.Interfaces.Localization;
using WebClient.Services.Interfaces.Logging;

namespace WebClient.Features.Abstracts;

public abstract class AppComponentBase : RazorComponentBase
{
    // Fields 
    private IAppLogger _appLogger;
    private IAppStringLocalizer _appStringLocalizer;

    // Properties
    [Inject]
    protected IMediator Mediator { get; set; }

    [Inject]
    protected IUserContext UserContext { get; set; }

    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    [Inject]
    private IAppLoggerFactory AppLoggerFactory { get; set; }

    [Inject]
    private IAppStringLocalizerFactory AppStringLocalizerFactory { get; set; }

    protected IAppLogger Logger
        => _appLogger ??= AppLoggerFactory.CreateLogger(GetType());

    protected IAppStringLocalizer Localizer
        => _appStringLocalizer ??= AppStringLocalizerFactory.Create(GetType());

}
