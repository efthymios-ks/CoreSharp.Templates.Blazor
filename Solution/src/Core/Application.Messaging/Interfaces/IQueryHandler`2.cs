using MediatR;

namespace Application.Messaging.Interfaces;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, IResult<TResponse>>
    where TQuery : IQuery<TResponse>
    where TResponse : class
{
}