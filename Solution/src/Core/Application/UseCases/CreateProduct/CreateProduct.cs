using Application.Messaging.Interfaces;
using CoreSharp.Templates.Blazor.Application.Dtos.Products;

namespace CoreSharp.Templates.Blazor.Application.UseCases.CreateProduct;

public class CreateProduct : ICommand<ProductDto>
{
    // Constructors
    public CreateProduct(ProductDto productDto)
        => ProductDto = productDto;

    // Properties
    public ProductDto ProductDto { get; }
}
