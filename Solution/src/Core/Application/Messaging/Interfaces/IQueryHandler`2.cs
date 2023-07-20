using CoreSharp.Templates.Blazor.Application.Messaging.Results;
using MediatR;

namespace CoreSharp.Templates.Blazor.Application.Messaging.Interfaces;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
    where TResponse : class
{
}