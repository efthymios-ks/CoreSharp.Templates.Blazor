using FluentValidation;

namespace CoreSharp.Templates.Blazor.Infrastructure.Configurations.Validators;

public sealed class ConnectionStringsOptionsValidator : AbstractValidator<ConnectionStringsOptions>
{
    public ConnectionStringsOptionsValidator()
        => AddRules();

    private void AddRules()
        => RuleFor(connectionStrings => connectionStrings.Database).NotEmpty();
}
