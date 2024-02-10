using System;
using System.Threading.Tasks;
using WebClient.Services.Interfaces;

namespace WebClient.Services;

public sealed class ErrorHandlingService : IErrorHandlingService
{
    // Methods
    public Task HandleAsync(string errorMessage)
        => Task.CompletedTask;

    public Task HandleAsync(Exception exception)
        => HandleAsync(exception?.Message);
}
