using PhonePlus.Common.Ports;
using PhonePlus.Interface.DTO.Credits;

namespace PhonePlus.Application.Ports.Credits.Input;

public class GetLatestCreditsInputPort : IInputPort<object, IEnumerable<CreditResponseDto>>
{
    public object RequestData { get; }
    public IOutputPort<IEnumerable<CreditResponseDto>> OutputPort { get; }
    
    public GetLatestCreditsInputPort(IOutputPort<IEnumerable<CreditResponseDto>> outputPort)
    {
        OutputPort = outputPort;
        RequestData = null; // No specific request data needed for this operation
    }
}