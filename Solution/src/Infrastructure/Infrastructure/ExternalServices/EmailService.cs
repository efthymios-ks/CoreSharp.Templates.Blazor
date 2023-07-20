using CoreSharp.Templates.Blazor.Application.Services.Interfaces.Logging;
using CoreSharp.Templates.Blazor.Infrastructure.ExternalServices.Interfaces;
using System;
using System.Threading.Tasks;

namespace CoreSharp.Templates.Blazor.Infrastructure.ExternalServices;

internal sealed class EmailService : IEmailService
{
    // Fields 
    private readonly IAppLogger _logger;

    // Constructors 
    public EmailService(IAppLogger<EmailService> logger)
        => _logger = logger;

    public Task SendEmailAsync()
    {
        try
        {
            // Demo implementation of sending email  
        }
        catch (Exception ex)
        {
            // Log exception 
            _logger.Error(ex, "Failed to send email.");
        }

        return Task.CompletedTask;
    }
}
