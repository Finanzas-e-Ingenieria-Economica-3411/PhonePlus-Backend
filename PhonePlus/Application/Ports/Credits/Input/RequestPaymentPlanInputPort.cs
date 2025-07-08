using MediatR;
using PhonePlus.Common.Ports;

namespace PhonePlus.Application.Ports.Credits.Input;

public class RequestPaymentPlanInputPort :IInputPort<int, IEnumerable<decimal>>
{
    public int RequestData { get; }
    public IOutputPort<IEnumerable<decimal>> OutputPort { get; }
    
   public RequestPaymentPlanInputPort(int requestData, IOutputPort<IEnumerable<decimal>> outputPort)
    {
        RequestData = requestData;
        OutputPort = outputPort;
    }
}