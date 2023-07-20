using CoreSharp.Templates.Blazor.Domain.Entities;
using CoreSharp.Templates.Blazor.Domain.Exceptions.Abstracts;
using System;

namespace CoreSharp.Templates.Blazor.Domain.Exceptions;

public sealed class ProductNotFoundException : AppEntityNotFoundException<Product, Guid>
{
    public ProductNotFoundException(Guid id)
        : base(id)
    {
    }
}

