using PhonePlus.Common.Ports;
using PhonePlus.Interface.DTO.Credits;

namespace PhonePlus.Application.Ports.Credits.Output;

public sealed class GetCreditsByUserIdOutputPort : IOutputPort<IEnumerable<CreditResponseDto>>
{
    public IEnumerable<CreditResponseDto> Data { get; private set; }

    public void Handle(IEnumerable<CreditResponseDto> response)
    {
        Data = response;
    }
}