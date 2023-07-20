using CoreSharp.Templates.Blazor.Application.Services.Interfaces.Localization;
using FluentValidation;

namespace CoreSharp.Templates.Blazor.Application.Messaging.Abstracts;

public abstract class AppValidatorBase<TValidatingType> : AbstractValidator<TValidatingType>
{
    // Fields
    private readonly IAppStringLocalizer _appStringLocalizer;

    protected AppValidatorBase(IAppStringLocalizerFactory appStringLocalizerFactory)
        => _appStringLocalizer = appStringLocalizerFactory.Create(GetType());

    protected string GetLocalizedString(string key)
        => _appStringLocalizer[key];
}