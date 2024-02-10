using CoreSharp.Templates.Blazor.Application.Messaging.Abstracts;
using CoreSharp.Templates.Blazor.Application.Services.Interfaces.Localization;
using FluentValidation;

namespace CoreSharp.Templates.Blazor.Application.UseCases.CreateProduct;

public sealed class CreateProductValidator : AppValidatorBase<CreateProduct>
{
    public CreateProductValidator(IAppStringLocalizerFactory appStringLocalizerFactory)
        : base(appStringLocalizerFactory)
        => AddRules();

    private void AddRules()
    {
        RuleFor(createProduct => createProduct.ProductDto)
            .NotNull()
            .WithMessage(GetLocalizedString("UnknownError"));

        RuleFor(createProduct => createProduct.ProductDto.Name)
            .NotEmpty();
    }
}
