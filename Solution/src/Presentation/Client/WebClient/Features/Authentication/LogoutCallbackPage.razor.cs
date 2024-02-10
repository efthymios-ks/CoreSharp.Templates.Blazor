using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebClient.Constants;

namespace WebClient.Features.Authentication;

public partial class LogoutCallbackPage
{
    // Methods 
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        // Do something after logging out

        NavigationManager.NavigateTo(PageRoutes.Home);
    }
}
