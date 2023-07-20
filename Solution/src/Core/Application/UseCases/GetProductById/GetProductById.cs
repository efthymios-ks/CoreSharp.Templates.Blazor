using CoreSharp.Templates.Blazor.Application.Dto;
using CoreSharp.Templates.Blazor.Application.Messaging.Abstracts;
using CoreSharp.Templates.Blazor.Application.Messaging.Interfaces;
using CoreSharp.Templates.Blazor.Domain.Entities;
using System;

namespace CoreSharp.Templates.Blazor.Application.UseCases.GetProductById;

public class GetProductById : RepositoryNavigationBase<Product>, IQuery<ProductDto>
{
    // Constructors
    public GetProductById(Guid productId)
        => ProductId = productId;

    // Properties
    public Guid ProductId { get; }
}
