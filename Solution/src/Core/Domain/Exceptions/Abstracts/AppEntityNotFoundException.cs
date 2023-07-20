using CoreSharp.EntityFramework.Entities.Abstracts;
using CoreSharp.Exceptions;

namespace CoreSharp.Templates.Blazor.Domain.Exceptions.Abstracts;

public abstract class AppEntityNotFoundException<TEntity, TKey> : EntityNotFoundException
    where TEntity : EntityBase<TKey>
{

    protected AppEntityNotFoundException(TKey id)
        : this(nameof(EntityBase<TKey>.Id), id)
    {
    }

    protected AppEntityNotFoundException(string propertyName, object propertyValue)
        : base(typeof(TEntity), propertyName, propertyValue)
    {
    }
}

