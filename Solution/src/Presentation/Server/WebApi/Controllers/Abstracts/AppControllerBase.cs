using CoreSharp.Templates.Blazor.Application.Services.Interfaces;
using CoreSharp.Templates.Blazor.Application.Services.Interfaces.Logging;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;

namespace WebApi.Controllers.Abstracts;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public abstract class AppControllerBase : ControllerBase
{
    // Fields
    private IAppLogger _logger;
    private IMediator _mediator;
    private IUserContext _userContext;

    // Properties 
    protected IAppLogger Logger
        => _logger ??= GetService<IAppLoggerFactory>()
                .CreateLogger(GetType());

    protected IMediator Mediator
        => _mediator ??= GetService<IMediator>();

    protected IUserContext UserContext
        => _userContext ??= GetService<IUserContext>();

    // Methods
    protected IActionResult ApiResult<TResult>(Application.Messaging.Interfaces.IResult<TResult> result)
    {
        if (result.IsOk)
        {
            return Ok(result);
        }

        return StatusCode(StatusCodes.Status500InternalServerError, result.Error);
    }

    private TService GetService<TService>()
        => HttpContext.RequestServices.GetRequiredService<TService>();
}
