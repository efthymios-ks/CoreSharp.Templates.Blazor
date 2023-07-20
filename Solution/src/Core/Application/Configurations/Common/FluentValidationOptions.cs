using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreSharp.Templates.Blazor.Application.Configurations.Common;

internal sealed class FluentValidationOptions<TOptions> : IValidateOptions<TOptions>
    where TOptions : class
{
    // Fields
    private readonly string _name;
    private readonly IServiceProvider _serviceProvider;

    // Constructors 
    public FluentValidationOptions(string name, IServiceProvider serviceProvider)
    {
        _name = name;
        _serviceProvider = serviceProvider;
    }

    // Methods 
    public ValidateOptionsResult Validate(string name, TOptions options)
    {
        // Null name is used to configure all named options. 
        if (_name != null && _name != name)
        {
            // Ignored if not validating this instance. 
            return ValidateOptionsResult.Skip;
        }

        ArgumentNullException.ThrowIfNull(options);

        using var serviceScope = _serviceProvider.CreateScope();
        var validator = serviceScope.ServiceProvider.GetService<IValidator<TOptions>>();
        if (validator is null)
        {
            return ValidateOptionsResult.Success;
        }

        var validationResult = validator.Validate(options);
        if (validationResult.IsValid)
        {
            return ValidateOptionsResult.Success;
        }

        var optionsType = options.GetType().Name;
        return ValidateOptionsResult.Fail(GetValidationErrors(optionsType, validationResult));
    }

    private static IEnumerable<string> GetValidationErrors(string optionsType, ValidationResult validationResult)
        => validationResult.Errors.Select(failure =>
            $"Options validation failed for '{optionsType}.{failure.PropertyName}' with error '{failure.ErrorMessage}'.");
}
