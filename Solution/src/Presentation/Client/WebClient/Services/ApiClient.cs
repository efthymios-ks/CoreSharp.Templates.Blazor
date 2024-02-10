using System.Net.Http;
using WebClient.Services.Interfaces;

namespace WebClient.Services;

public sealed class ApiClient : IApiClient
{
    // Constructors 
    public ApiClient(HttpClient httpClient)
        => HttpClient = httpClient;

    // Properties 
    public HttpClient HttpClient { get; }
}