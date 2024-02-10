using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using WebClient.Features.Abstracts;

namespace WebClient.Features.Authentication;

public sealed class LogoutView : AppComponentBase
{
    [Inject]
    private IConfiguration Configuration { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        var options = Configuration.GetSection("Authentication:Auth0");
        var authority = options["Authority"];
        var clientId = options["ClientId"];
        NavigationManager.NavigateTo($"{authority}/v2/logout?client_id={clientId}");
    }
}
