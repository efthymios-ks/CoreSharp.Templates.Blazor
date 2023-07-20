using System.Threading.Tasks;

namespace CoreSharp.Templates.Blazor.Infrastructure.ExternalServices.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync();
}