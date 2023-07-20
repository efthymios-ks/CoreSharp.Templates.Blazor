using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Controllers.Abstracts;

namespace WebApp.Controllers.V1;

[ApiVersion("1")]
public sealed class ProductsController : AppControllerBase
{
    // Methods 
    [HttpGet]
    public Task<string> GetAsync()
        => Task.FromResult($"{nameof(GetAsync)} > V1");
}