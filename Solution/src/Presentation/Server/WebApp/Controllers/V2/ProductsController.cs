using CoreSharp.Templates.Blazor.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Controllers.Abstracts;

namespace WebApp.Controllers.V2;

[ApiVersion("2")]
public sealed class ProductsController : AppControllerBase
{
    // Methods 
    [HttpGet]
    public Task<string> GetAsync()
        => Task.FromResult($"{nameof(GetAsync)} > V2");

    [HttpPost]
    public Task CreateAsync(ProductDto product)
        => Task.CompletedTask;
}