using PhonePlus.Common.Ports;
using PhonePlus.Interface.DTO.Credits;

namespace PhonePlus.Application.Ports.Credits.Input;

public sealed class UpdateCreditInputPort : IInputPort<UpdateCreditRequestDto, bool>
{
    public UpdateCreditRequestDto RequestData { get; }
    public IOutputPort<bool> OutputPort { get; }

    public UpdateCreditInputPort(UpdateCreditRequestDto requestData, IOutputPort<bool> outputPort)
    {
        RequestData = requestData;
        OutputPort = outputPort;
    }
}

