using CoreSharp.EntityFramework.Repositories.Interfaces;

namespace CoreSharp.Templates.Blazor.Domain.Repositories;

public interface IAppUnitOfWork : IUnitOfWork
{
    // Properties
    IProductRepository ProductRepository { get; }
}
