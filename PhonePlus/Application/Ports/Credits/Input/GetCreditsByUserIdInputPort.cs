using PhonePlus.Common.Ports;
using PhonePlus.Interface.DTO.Credits;

namespace PhonePlus.Application.Ports.Credits.Input;

public sealed class GetCreditsByUserIdInputPort : IInputPort<int, IEnumerable<CreditResponseDto>>
{
    public int RequestData { get; }
    public IOutputPort<IEnumerable<CreditResponseDto>> OutputPort { get; }
    
    public GetCreditsByUserIdInputPort(int requestData, IOutputPort<IEnumerable<CreditResponseDto>> outputPort)
    {
        RequestData = requestData;
        OutputPort = outputPort;
    }
}