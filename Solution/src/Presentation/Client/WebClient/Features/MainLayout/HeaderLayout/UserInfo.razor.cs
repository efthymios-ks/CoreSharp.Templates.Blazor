using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using WebClient.Constants;
using WebClient.Services.Interfaces;

namespace WebClient.Features.MainLayout.HeaderLayout;

public partial class UserInfo : IDisposable
{
    private bool _isDisposed;

    // Properties
    [Inject]
    private IUserContext UserContext { get; set; }

    private static string MyClaimsPath
        => PageRoutes.Users.MyClaims;

    private string Username
        => UserContext.FullName;

    private bool ShouldDisplayUserPhoto
        => !string.IsNullOrWhiteSpace(UserPhotoUrl);

    private string UserPhotoUrl
        => UserContext.PhotoUrl;

    // Methods
    private void OnLoginClicked()
        => NavigationManager.NavigateTo(PageRoutes.Authentication.Login);

    private void OnLogoutClicked()
        => NavigationManager.NavigateToLogout(PageRoutes.Authentication.Logout);

    // Methods 
    protected override void OnInitialized()
    {
        base.OnInitialized();

        UserContext.Changed += AuthenticationChangedCallback;
    }

    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        UserContext.Changed -= AuthenticationChangedCallback;
        _isDisposed = true;
    }

    private void AuthenticationChangedCallback()
        => StateHasChanged();
}
