using MediatR;
using PhonePlus.Application.Ports.Credits.Input;
using PhonePlus.Application.Ports.Credits.Output;
using PhonePlus.Common.Repository;
using PhonePlus.Domain.Models;
using PhonePlus.Domain.Repositories;
using PhonePlus.Domain.Enums;
using PhonePlus.Interface.DTO.Credits;
using System.Linq;

namespace PhonePlus.Application.UseCases.Credits;

public sealed class UpdateCreditUseCase(
    ICreditRepository creditRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdateCreditInputPort>
{
    public async Task Handle(UpdateCreditInputPort request, CancellationToken cancellationToken)
    {
        try
        {
            var dto = request.RequestData;
            var credit = await creditRepository.FindByIdAsync(dto.Id);
            if (credit == null)
            {
                throw new Exception("Credit not found");
            }

            credit.GetType().GetProperty("ComercialValue")?.SetValue(credit, dto.ComercialValue);
            credit.GetType().GetProperty("NominalValue")?.SetValue(credit, dto.NominalValue);
            credit.GetType().GetProperty("StructurationRate")?.SetValue(credit, dto.StructurationRate);
            credit.GetType().GetProperty("ColonRate")?.SetValue(credit, dto.ColonRate);
            credit.GetType().GetProperty("FlotationRate")?.SetValue(credit, dto.FlotationRate);
            credit.GetType().GetProperty("CavaliRate")?.SetValue(credit, dto.CavaliRate);
            credit.GetType().GetProperty("PrimRate")?.SetValue(credit, dto.PrimRate);
            credit.GetType().GetProperty("NumberOfYears")?.SetValue(credit, dto.NumberOfYears);
            credit.GetType().GetProperty("Frequencies")?.SetValue(credit, dto.Frequencies);
            credit.GetType().GetProperty("DayPerYear")?.SetValue(credit, dto.DayPerYear);
            credit.GetType().GetProperty("CapitalizationTypes")?.SetValue(credit, dto.CapitalizationTypes);
            credit.GetType().GetProperty("UserId")?.SetValue(credit, dto.UserId);
            credit.GetType().GetProperty("CuponRate")?.SetValue(credit, dto.CuponRate);
            credit.GetType().GetProperty("CuponRateType")?.SetValue(credit, dto.CuponRateType);
            credit.GetType().GetProperty("CuponRateFrequency")?.SetValue(credit, dto.CuponRateFrequency);
            credit.GetType().GetProperty("CuponRateCapitalization")?.SetValue(credit, dto.CuponRateCapitalization);
            credit.GetType().GetProperty("Currency")?.SetValue(credit, dto.Currency);
            
            credit.GracePeriods.RemoveAll(gp => !dto.GracePeriods.Any(ngp => ngp.Period == gp.Period && ngp.Type == gp.Type));
            foreach (var ngp in dto.GracePeriods)
            {
                var existing = credit.GracePeriods.FirstOrDefault(gp => gp.Period == ngp.Period && gp.Type == ngp.Type);
                if (existing == null)
                {
                    credit.GracePeriods.Add(new GracePeriod { Period = ngp.Period, Type = ngp.Type, CreditId = credit.Id });
                }
            }

            creditRepository.Update(credit);
            await unitOfWork.CompleteAsync();
            request.OutputPort.Handle(true);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating credit: {ex.Message}");
        }
    }
}
