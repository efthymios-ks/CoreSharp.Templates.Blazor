using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace WebApp.Features;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
    // Properties
    public string RequestId { get; set; }

    public bool ShowRequestId
        => !string.IsNullOrEmpty(RequestId);

    // Methods 
    public void OnGet()
        => RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
}
