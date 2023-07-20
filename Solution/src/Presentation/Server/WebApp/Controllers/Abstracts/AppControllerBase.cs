using CoreSharp.Templates.Blazor.Application.Services.Interfaces;
using CoreSharp.Templates.Blazor.Application.Services.Interfaces.Logging;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;

namespace WebApp.Controllers.Abstracts;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public abstract class AppControllerBase : ControllerBase
{
    // Fields
    private IAppLogger _logger;
    private IMediator _mediator;
    private ICurrentIdentityService _currentIdentityService;

    // Properties 
    protected IAppLogger Logger
        => _logger ??= GetService<IAppLoggerFactory>()
                        .CreateLogger(GetType());

    protected IMediator Mediator
        => _mediator ??= GetService<IMediator>();

    protected ICurrentIdentityService CurrentIdentityService
        => _currentIdentityService ??= GetService<ICurrentIdentityService>();

    // Methods
    private TService GetService<TService>()
        => HttpContext.RequestServices.GetRequiredService<TService>();
}