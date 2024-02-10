using FluentValidation;

namespace CoreSharp.Templates.Blazor.Infrastructure.Configurations.Validators;

public sealed class Auth0OptionsValidator : AbstractValidator<Auth0Options>
{
    public Auth0OptionsValidator()
        => AddRules();

    private void AddRules()
    {
        RuleFor(auth0 => auth0.Authority)
            .NotEmpty();
        RuleFor(auth0 => auth0.Audience)
            .NotEmpty();
        RuleFor(auth0 => auth0.Scopes)
            .NotNull();
    }
}
