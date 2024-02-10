using CoreSharp.Templates.Blazor.Application.Dtos.Products;
using CoreSharp.Templates.Blazor.Application.Mappings.Abstracts;
using CoreSharp.Templates.Blazor.Domain.Entities;

namespace CoreSharp.Templates.Blazor.Application.Mappings;

internal sealed class ProductProfile : TwoWayProfileBase<Product, ProductDto>
{
}
