using PhonePlus.Common.Ports;

namespace PhonePlus.Application.Ports.Credits.Output;

public class RequestPaymentPlanOutputPort : IOutputPort<IEnumerable<decimal>>
{
    
    public IEnumerable<decimal> Data { get; private set; }
    public void Handle(IEnumerable<decimal> response)
    {
        Data = response;
    }
}