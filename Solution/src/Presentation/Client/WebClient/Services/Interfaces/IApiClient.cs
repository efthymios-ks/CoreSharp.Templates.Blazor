using System.Net.Http;

namespace WebClient.Services.Interfaces;

public interface IApiClient
{
    HttpClient HttpClient { get; }
}
