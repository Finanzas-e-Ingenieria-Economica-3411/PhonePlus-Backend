using MediatR;
using PhonePlus.Application.Ports.Notifications;
using PhonePlus.Application.Ports.Notifications.Input;
using PhonePlus.Domain.Repositories;
using PhonePlus.Interface.DTO.Credits;

namespace PhonePlus.Application.UseCases.Credits;

public sealed class GetCreditsByStateIdUseCase(ICreditRepository creditRepository) : IRequestHandler<GetCreditsByStateIdInputPort>
{
    public async Task Handle(GetCreditsByStateIdInputPort request, CancellationToken cancellationToken)
    {
        var credits = await creditRepository.GetCreditsByStateId(request.RequestData);
        var creditResponse = credits.Select(credit => new CreditResponseDto(
            credit.Id,
            credit.PhoneNumber,
            credit.Price,
            credit.StartDate,
            credit.Months,
            credit.InterestRate,
            credit.Insurance,
            credit.Amortization,
            credit.Paid,
            credit.Interest,
            credit.PendingPayment,
            credit.UserId,
            credit.StateId
        )).ToList();
        request.OutputPort.Handle(creditResponse);
    }
}