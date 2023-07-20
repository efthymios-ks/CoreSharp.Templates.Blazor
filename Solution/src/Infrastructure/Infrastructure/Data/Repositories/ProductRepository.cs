using CoreSharp.EntityFramework.Repositories.Abstracts;
using CoreSharp.Templates.Blazor.Domain.Entities;
using CoreSharp.Templates.Blazor.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoreSharp.Templates.Blazor.Infrastructure.Data.Repositories;

internal sealed class ProductRepository : ExtendedRepositoryBase<Product>, IProductRepository
{
    // Constructors
    public ProductRepository(DbContext dbContext)
        : base(dbContext)
    {
    }
}
