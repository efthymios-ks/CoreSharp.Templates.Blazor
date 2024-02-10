using Microsoft.AspNetCore.Components;

namespace WebClient.Features.MainLayout.ContentLayout;

public partial class Content
{
    // Properties
    [Parameter]
    public RenderFragment ChildContent { get; set; }
}
