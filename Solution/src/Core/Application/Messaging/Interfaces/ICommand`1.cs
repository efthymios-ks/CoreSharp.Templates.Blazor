using CoreSharp.Templates.Blazor.Application.Messaging.Results;
using MediatR;

namespace CoreSharp.Templates.Blazor.Application.Messaging.Interfaces;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    where TResponse : class
{
}
