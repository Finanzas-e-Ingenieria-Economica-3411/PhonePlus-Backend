using MediatR;
using PhonePlus.Application.Ports.Credits.Input;
using PhonePlus.Domain.Models;
using PhonePlus.Domain.Repositories;
using PhonePlus.Interface.DTO.Credits;

namespace PhonePlus.Application.UseCases.Credits;

public sealed class GetCreditsByStateIdUseCase(ICreditRepository creditRepository, IUserRepository userRepository) : IRequestHandler<GetCreditsByStateIdInputPort>
{
    public async Task Handle(GetCreditsByStateIdInputPort request, CancellationToken cancellationToken)
    {
        var credits = await creditRepository.GetCreditsByStateId(request.RequestData);
        var userIds = credits.Select(credit => credit.UserId).Distinct().ToList();
        var users = new List<User>();
        foreach (var userId in userIds)
        {
            var user = await userRepository.FindByIdAsync(userId);
            if (user != null)
            {
                users.Add(user);
            }
        }

        var creditResponse = credits.Select(credit =>
        {
            var clientName = users.FirstOrDefault(u => u.Id == credit.UserId)?.Name;
            var username = users.FirstOrDefault(u => u.Id == credit.UserId)?.Username;
                return new CreditResponseDto(
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
                    clientName,
                    username,
                    credit.StateId
                );
        }).ToList();
        request.OutputPort.Handle(creditResponse);
    }
}