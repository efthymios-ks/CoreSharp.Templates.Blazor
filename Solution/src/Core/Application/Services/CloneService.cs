using CoreSharp.Extensions;
using CoreSharp.Templates.Blazor.Application.Services.Interfaces;
using System.Threading.Tasks;

namespace CoreSharp.Templates.Blazor.Application.Services;

public sealed class CloneService : ICloneService
{
    public Task<TEntity> CloneAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        var clonedEntity = entity?.JsonClone();
        return Task.FromResult(clonedEntity);
    }
}