using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers.Abstracts;

[Authorize]
public abstract class AppAuthenticatedControllerBase : AppControllerBase
{
}