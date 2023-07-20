using CoreSharp.Templates.Blazor.Application.Dto;
using CoreSharp.Templates.Blazor.Application.Messaging.Interfaces;

namespace CoreSharp.Templates.Blazor.Application.UseCases.CreateProduct;

public class CreateProduct : ICommand<ProductDto>
{
    // Constructors
    public CreateProduct(ProductDto productDto)
        => ProductDto = productDto;

    // Properties
    public ProductDto ProductDto { get; }
}
