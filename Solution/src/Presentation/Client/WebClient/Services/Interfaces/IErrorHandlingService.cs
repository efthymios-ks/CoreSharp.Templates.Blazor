using System;
using System.Threading.Tasks;

namespace WebClient.Services.Interfaces;

public interface IErrorHandlingService
{
    Task HandleAsync(string errorMessage);
    Task HandleAsync(Exception exception);
}