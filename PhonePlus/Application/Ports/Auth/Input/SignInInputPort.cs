using PhonePlus.Common.Ports;
using PhonePlus.Interface.DTO.Auth;
using PhonePlus.Interface.DTO.User;

namespace PhonePlus.Application.Ports.Auth.Input;

public sealed class SignInInputPort : IInputPort<SignInDto,UserAuthenticatedResponseDto>
{
    public SignInDto RequestData { get; }
    public IOutputPort<UserAuthenticatedResponseDto> OutputPort { get; }
    
    public SignInInputPort(SignInDto requestData, IOutputPort<UserAuthenticatedResponseDto> outputPort)
    {
        RequestData = requestData;
        OutputPort = outputPort;
    }
}