using PhonePlus.Common.Ports;
using PhonePlus.Interface.DTO.Credits;

namespace PhonePlus.Application.Ports.Credits.Input;

public sealed class UpdateCreditStateInputPort : IInputPort<UpdateCreditStateDto,bool>
{
    public UpdateCreditStateDto RequestData { get; }
    public IOutputPort<bool> OutputPort { get; }
    
    public UpdateCreditStateInputPort(UpdateCreditStateDto requestData, IOutputPort<bool> outputPort)
    {
        RequestData = requestData;
        OutputPort = outputPort;
    }
}