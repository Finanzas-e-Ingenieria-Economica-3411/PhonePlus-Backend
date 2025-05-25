using PhonePlus.Common.Ports;
using PhonePlus.Interface.DTO.Auth;

namespace PhonePlus.Application.Ports.Auth;

public sealed class SignUpInputPort : IInputPort<SignUpDto, bool>
{
    public SignUpDto RequestData { get; }
    public IOutputPort<bool> OutputPort { get; }
    
    public SignUpInputPort(SignUpDto requestData, IOutputPort<bool> outputPort)
    {
        RequestData = requestData;
        OutputPort = outputPort;
    }
}