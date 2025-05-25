using PhonePlus.Common.Ports;
using PhonePlus.Interface.DTO.User;

namespace PhonePlus.Application.Ports.Auth.Output;

public sealed class SignInOutputPort : IOutputPort<UserAuthenticatedResponseDto>
{
    public UserAuthenticatedResponseDto Content { get; private set; }
    public void Handle(UserAuthenticatedResponseDto response)
    {
        Content = response;
    }
}