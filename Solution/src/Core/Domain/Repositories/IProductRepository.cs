using CoreSharp.Templates.Blazor.Domain.Entities;
using CoreSharp.Templates.Blazor.Domain.Repositories.Common;
using System;

namespace CoreSharp.Templates.Blazor.Domain.Repositories;

public interface IProductRepository : IAppRepository<Product, Guid>
{
}
