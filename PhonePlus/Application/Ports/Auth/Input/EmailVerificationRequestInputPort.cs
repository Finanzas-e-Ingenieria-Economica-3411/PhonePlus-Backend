using PhonePlus.Common.Ports;

namespace PhonePlus.Application.Ports.Auth.Input;

public sealed class EmailVerificationRequestInputPort : IInputPort<string,bool>
{
    public string RequestData { get; }
    public IOutputPort<bool> OutputPort { get; }
    
    public EmailVerificationRequestInputPort(
        string requestData, 
        IOutputPort<bool> outputPort)
    {
        RequestData = requestData;
        OutputPort = outputPort;
    }
}