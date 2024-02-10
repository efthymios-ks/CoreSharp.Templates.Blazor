using Application.Messaging;
using Application.Messaging.Interfaces;
using CoreSharp.Http.FluentApi.Extensions;
using CoreSharp.Templates.Blazor.Application.Dtos.Users;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebClient.Services.Interfaces;

namespace WebClient.UseCases.GetOrAddUser;

internal sealed class GetOrAddUserCommandHandler : ICommandHandler<GetOrAddUserCommand, UserDto>
{
    // Fields
    private readonly HttpClient _httpClient;

    public GetOrAddUserCommandHandler(IApiClient apiClient)
        => _httpClient = apiClient.HttpClient;

    // Methods
    public async Task<IResult<UserDto>> Handle(GetOrAddUserCommand request, CancellationToken cancellationToken)
        => await _httpClient
            .Request()
            .Route("users")
            .Post()
            .JsonContent(request.CreateUserDto)
            .Json<Result<UserDto>>()
            .SendAsync(cancellationToken);
}