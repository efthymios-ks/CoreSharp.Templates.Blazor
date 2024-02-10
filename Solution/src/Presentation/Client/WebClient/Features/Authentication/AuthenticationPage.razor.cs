using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebClient.Services.Interfaces;

namespace WebClient.Features.Authentication;

public partial class AuthenticationPage
{
    // Properties
    [Parameter]
    public string Action { get; set; }

    // Properties 
    [Inject]
    private IUserContext UserContext { get; set; }

    [Inject]
    private IErrorHandlingService ErrorHandlingService { get; set; }

    private Task LoginSucceededCallbackAsync()
        => Task.CompletedTask;
}