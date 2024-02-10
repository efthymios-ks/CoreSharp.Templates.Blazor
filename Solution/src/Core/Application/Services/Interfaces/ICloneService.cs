using System.Threading.Tasks;

namespace CoreSharp.Templates.Blazor.Application.Services.Interfaces;

public interface ICloneService
{
    Task<TEntity> CloneAsync<TEntity>(TEntity entity)
        where TEntity : class;
}
