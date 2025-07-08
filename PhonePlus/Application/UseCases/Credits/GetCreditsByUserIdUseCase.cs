using MediatR;
using PhonePlus.Application.Ports.Credits.Input;
using PhonePlus.Domain.Models;
using PhonePlus.Domain.Repositories;
using PhonePlus.Interface.DTO.Credits;

namespace PhonePlus.Application.UseCases.Credits;

public sealed class GetCreditsByUserIdUseCase(ICreditRepository creditRepository, IUserRepository userRepository) : IRequestHandler<GetCreditsByUserIdInputPort>
{
    public async Task Handle(GetCreditsByUserIdInputPort request, CancellationToken cancellationToken)
    {
        var credits = await creditRepository.GetCreditsByUserIdAsync(request.RequestData);
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
                credit.ComercialValue,
                credit.NominalValue,
                credit.StructurationRate,
                credit.ColonRate,
                credit.FlotationRate,
                credit.CavaliRate,
                credit.PrimRate,
                credit.NumberOfYears,
                credit.State,
                credit.Frequencies,
                credit.DayPerYear,
                credit.InterestRates,
                credit.CapitalizationTypes,
                credit.InterestRate,
                credit.YearDiscount,
                credit.RentImport,
                credit.UserId,
                credit.CuponRate,
                credit.Currency,
                clientName ?? string.Empty,
                username ?? string.Empty
            );
        }).ToList();
        request.OutputPort.Handle(creditResponse);
    }
}