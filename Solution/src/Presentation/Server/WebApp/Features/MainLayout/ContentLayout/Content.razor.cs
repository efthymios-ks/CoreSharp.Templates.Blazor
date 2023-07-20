using Microsoft.AspNetCore.Components;

namespace WebApp.Features.MainLayout.ContentLayout;

public partial class Content
{
    // Properties
    [Parameter]
    public RenderFragment ChildContent { get; set; }
}
