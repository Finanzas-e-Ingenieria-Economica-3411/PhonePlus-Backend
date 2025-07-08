using MediatR;
using PhonePlus.Common.Ports;
using PhonePlus.Domain.Enums;
using PhonePlus.Interface.DTO.Credits;

namespace PhonePlus.Application.Ports.Credits.Input;

public class RequestPaymentPlanInputPort : IInputPort<PaymentPlanRequestDto, BondIndicatorsDto>
{
    public PaymentPlanRequestDto RequestData { get; }
    public IOutputPort<BondIndicatorsDto> OutputPort { get; }

    public RequestPaymentPlanInputPort(PaymentPlanRequestDto requestData, IOutputPort<BondIndicatorsDto> outputPort)
    {
        RequestData = requestData;
        OutputPort = outputPort;
    }
}