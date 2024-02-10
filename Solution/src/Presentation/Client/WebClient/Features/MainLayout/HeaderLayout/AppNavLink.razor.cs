using Microsoft.AspNetCore.Components;

namespace WebClient.Features.MainLayout.HeaderLayout;

public partial class AppNavLink
{
    // Properties
    [Parameter]
    public string Text { get; set; }

    [Parameter]
    public string RedirectTo { get; set; }
}
