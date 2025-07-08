using MediatR;
using PhonePlus.Application.Ports.Credits.Input;
using PhonePlus.Domain.Enums;
using PhonePlus.Domain.Models;
using PhonePlus.Domain.Repositories;

namespace PhonePlus.Application.UseCases.Credits;

public sealed class RequestPaymentPlanUseCase(ICreditRepository creditRepository) : IRequestHandler<RequestPaymentPlanInputPort>
{
    public async Task Handle(RequestPaymentPlanInputPort request, CancellationToken cancellationToken)
{
    try
    {
        var credit = await creditRepository.GetCreditByIdAsync(request.RequestData);
        if (credit == null)
        {
            throw new Exception("Credit not found.");
        }

        var rate = CalculateCuponRate(credit.InterestRate, credit);

        var paymentPlan = new List<decimal>();
        var time = CalculateTimePerCapitalization(credit.CapitalizationTypes, credit.Frequencies);
        var totalPeriods = credit.NumberOfYears *
            (time);
        if (credit is { StructurationRate: not null, CavaliRate: not null })
        {
            if (credit is { FlotationRate: not null, ColonRate: not null })
            {
                decimal flow0 = (-1 * credit.ComercialValue) * (1 + (credit.FlotationRate.Value / 100) + (credit.CavaliRate.Value / 100));
                paymentPlan.Add(flow0);
            }
        }

        for (int i = 1; i < totalPeriods; i++)
        {
            var flow = credit.NominalValue * rate;
            paymentPlan.Add(flow);
        }
        
        if (credit.PrimRate != null)
        {
            var lastFlow = credit.NominalValue * (rate + (credit.PrimRate.Value / 100)) + credit.NominalValue;
            paymentPlan.Add(lastFlow);
        }
        
        request.OutputPort.Handle(paymentPlan);
    }
    catch (Exception ex)
    {
        throw new Exception(ex.Message);
    }
}

    private decimal CalculateCuponRate(decimal interest, Credit credit)
    {
        if (credit.CapitalizationTypes == CapitalizationTypes.Annual)
        {
            return interest / 100;
        }
        var days = CalculateDaysPerYear(credit.Frequencies);
        
        var rate = Math.Pow((double)(1 + (interest / 100)), (double)days / 360) - 1;

        Console.WriteLine($"Calculated rate: {rate}");
        return (decimal)rate;
    }



private int CalculateTimePerCapitalization(CapitalizationTypes capitalizationTypes, Frequencies frequencies)
{
    return capitalizationTypes switch
    {
        CapitalizationTypes.Cuatrimester => 4,
        CapitalizationTypes.Semester => 2,
        CapitalizationTypes.Annual => 1,
        CapitalizationTypes.Bimonthly => 6,
        CapitalizationTypes.Monthly => CalculateMonthsPerYear(frequencies),
        CapitalizationTypes.Weekly => 52, 
        CapitalizationTypes.Daily =>  CalculateDaysPerYear(frequencies),
        _ => throw new ArgumentOutOfRangeException(nameof(capitalizationTypes), "Invalid capitalization type.")
    };
}



private int CalculateMonthsPerYear(Frequencies frequency)
{
    return frequency switch
    {
        Frequencies.Bimester => 6,
        Frequencies.Trimestre => 4,
        Frequencies.Year => 1,
        Frequencies.FifteenDays => 24,
        Frequencies.Monthly => 12,
        Frequencies.Quarterly => 4,
        Frequencies.Semestral => 2,
        _ => throw new ArgumentOutOfRangeException(nameof(frequency), "Invalid frequency type.")
    };
}

private int CalculateDaysPerYear(Frequencies frequency)
{
    return frequency switch
    {
        Frequencies.Bimester => 60,
        Frequencies.Trimestre => 90,
        Frequencies.Year => 360,
        Frequencies.FifteenDays => 360,
        Frequencies.Monthly => 30,
        Frequencies.Quarterly => 90,
        Frequencies.Semestral => 180,
        _ => throw new ArgumentOutOfRangeException(nameof(frequency), "Invalid frequency type.")
    };
}


}