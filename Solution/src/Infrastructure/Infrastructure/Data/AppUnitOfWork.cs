using CoreSharp.EntityFramework.Repositories.Abstracts;
using CoreSharp.Templates.Blazor.Domain.Repositories;
using CoreSharp.Templates.Blazor.Infrastructure.Data.Repositories;

namespace CoreSharp.Templates.Blazor.Infrastructure.Data;

internal sealed class AppUnitOfWork : UnitOfWorkBase, IAppUnitOfWork
{
    // Fields 
    private IProductRepository _productRepository;

    // Constructors
    public AppUnitOfWork(AppDbContext dbContext)
        : base(dbContext)
    {
    }

    // Properties
    public IProductRepository ProductRepository
        => _productRepository ??= new ProductRepository(Context);
}
