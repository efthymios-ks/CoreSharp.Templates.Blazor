using MediatR;

namespace Application.Messaging.Interfaces;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, IResult<TResponse>>
    where TCommand : ICommand<TResponse>
    where TResponse : class
{
}
