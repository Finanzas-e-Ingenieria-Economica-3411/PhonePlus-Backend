using PhonePlus.Common.Ports;
using PhonePlus.Interface.DTO.Credits;

namespace PhonePlus.Application.Ports.Credits.Input;

public sealed class CreateCreditInputPort : IInputPort<CreateCreditRequestDto,bool>
{
    public CreateCreditRequestDto RequestData { get; }
    public IOutputPort<bool> OutputPort { get; }
    
    public CreateCreditInputPort(CreateCreditRequestDto requestData, IOutputPort<bool> outputPort)
    {
        RequestData = requestData;
        OutputPort = outputPort;
    }
}