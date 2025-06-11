using PhonePlus.Common.Ports;
using PhonePlus.Interface.DTO.Credits;

namespace PhonePlus.Application.Ports.Notifications.Input;

public sealed class GetCreditsByStateIdInputPort : IInputPort<int,IEnumerable<CreditResponseDto>>
{
    public int RequestData { get; }
    public IOutputPort<IEnumerable<CreditResponseDto>> OutputPort { get; }
    
    public GetCreditsByStateIdInputPort(int requestData, IOutputPort<IEnumerable<CreditResponseDto>> outputPort)
    {
        RequestData = requestData;
        OutputPort = outputPort;
    }
}