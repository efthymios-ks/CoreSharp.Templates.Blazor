using MediatR;

namespace CoreSharp.Templates.Blazor.Application.Messaging.Interfaces;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : class, ICommand
{
}
