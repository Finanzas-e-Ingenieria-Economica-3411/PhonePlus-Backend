using PhonePlus.Common.Ports;
using PhonePlus.Interface.DTO.Credits;

namespace PhonePlus.Application.Ports.Credits.Output;

public class RequestPaymentPlanOutputPort : IOutputPort<BondIndicatorsDto>
{
    public BondIndicatorsDto Data { get; private set; }
    public void Handle(BondIndicatorsDto response)
    {
        Data = response;
    }
}