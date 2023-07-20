using CoreSharp.Templates.Blazor.Application.Configurations.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace CoreSharp.Templates.Blazor.Application.Extensions;

/// <summary>
/// <see cref="OptionsBuilder{TOptions}"/> extensions.
/// </summary>
public static class OptionsBuilderExtensions
{
    public static OptionsBuilder<TOptions> ValidateFluently<TOptions>(
        this OptionsBuilder<TOptions> optionsBuilder)
            where TOptions : class
    {
        optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(GetFluentValidationOptions);
        return optionsBuilder;

        FluentValidationOptions<TOptions> GetFluentValidationOptions(IServiceProvider serviceProvider)
            => new(optionsBuilder.Name, serviceProvider);
    }
}