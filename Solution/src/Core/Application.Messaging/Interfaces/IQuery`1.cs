using MediatR;

namespace Application.Messaging.Interfaces;

public interface IQuery<TResponse> : IRequest<IResult<TResponse>>
    where TResponse : class
{
}
