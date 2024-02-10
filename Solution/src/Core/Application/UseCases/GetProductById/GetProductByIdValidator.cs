using CoreSharp.Templates.Blazor.Application.Messaging.Abstracts;
using CoreSharp.Templates.Blazor.Application.Services.Interfaces.Localization;
using FluentValidation;

namespace CoreSharp.Templates.Blazor.Application.UseCases.GetProductById;

public sealed class GetProductByIdValidator : AppValidatorBase<GetProductById>
{
    public GetProductByIdValidator(IAppStringLocalizerFactory appStringLocalizerFactory)
        : base(appStringLocalizerFactory)
        => AddRules();

    private void AddRules()
        => RuleFor(getProductBy => getProductBy.ProductId)
            .NotEmpty();
}
