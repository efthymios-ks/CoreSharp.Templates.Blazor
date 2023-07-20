using CoreSharp.EntityFramework.Repositories.Interfaces;
using CoreSharp.Templates.Blazor.Domain.Entities.Abstracts;

namespace CoreSharp.Templates.Blazor.Domain.Repositories.Common;

public interface IAppRepository<TEntity, TKey> : IExtendedRepository<TEntity>
    where TEntity : AppEntityBase<TKey>
{

}
