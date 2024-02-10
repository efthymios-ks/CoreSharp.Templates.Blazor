using Application.Messaging.Interfaces;
using CoreSharp.Templates.Blazor.Application.Dtos.Users;

namespace WebClient.UseCases.GetOrAddUser;

public sealed class GetOrAddUserCommand : ICommand<UserDto>
{
    // Constructors 
    public GetOrAddUserCommand(CreateUserDto createUserDto)
        => CreateUserDto = createUserDto;

    // Properties
    public CreateUserDto CreateUserDto { get; set; }
}
