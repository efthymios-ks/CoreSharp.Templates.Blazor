using MediatR;

namespace Application.Messaging.Interfaces;

public interface ICommand<TResponse> : IRequest<IResult<TResponse>>
    where TResponse : class
{
}
