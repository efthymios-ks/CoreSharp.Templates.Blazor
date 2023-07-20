using CoreSharp.EntityFramework.Delegates;

namespace CoreSharp.Templates.Blazor.Application.Messaging.Abstracts;

public abstract class RepositoryNavigationBase<TEntity>
{
    // Properties
    public Query<TEntity> Navigation { get; init; }
}
