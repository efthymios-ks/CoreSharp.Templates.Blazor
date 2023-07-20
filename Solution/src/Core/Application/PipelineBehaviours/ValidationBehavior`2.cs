using CoreSharp.Templates.Blazor.Application.Messaging.Results;
using CoreSharp.Templates.Blazor.Application.Messaging.Results.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreSharp.Templates.Blazor.Application.PipelineBehaviours;

internal sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : class, IResult
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        => _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var validationContext = new ValidationContext<TRequest>(request);

        var validationErrors = _validators
            .Select(validator => validator.Validate(validationContext))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationError => validationError != null);

        if (validationErrors.Any())
        {
            // Handle Request<> with reflection.
            // Generic constraints didn't work out. 
            if (typeof(TResponse).IsGenericType
                && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                var errorMessage = validationErrors.First().ErrorMessage;
                var resultAsObject = Activator.CreateInstance(typeof(TResponse), default, errorMessage);
                return resultAsObject as TResponse;
            }

            // Throw if not handled with Result<>. 
            throw new ValidationException(validationErrors);
        }

        return await next();
    }
}