using CoreSharp.Templates.Blazor.Application.Messaging.Results;
using MediatR;

namespace CoreSharp.Templates.Blazor.Application.Messaging.Interfaces;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
    where TResponse : class
{
}
