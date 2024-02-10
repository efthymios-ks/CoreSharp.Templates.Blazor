using CoreSharp.Templates.Blazor.Application.Dtos.Products;
using CoreSharp.Templates.Blazor.Application.UseCases.CreateProduct;
using CoreSharp.Templates.Blazor.Application.UseCases.GetProductById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi.Controllers.Abstracts;

namespace WebApi.Controllers.V1;

[ApiVersion("1")]
public sealed class ProductsController : AppAuthenticatedControllerBase
{
    // Methods 
    [HttpGet("{teacherId}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync(Guid productId)
    {
        var result = await Mediator.Send(new GetProductById(productId));
        return ApiResult(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddAsync(ProductDto productToCreateDto)
    {
        var result = await Mediator.Send(new CreateProduct(productToCreateDto));
        return ApiResult(result);
    }
}