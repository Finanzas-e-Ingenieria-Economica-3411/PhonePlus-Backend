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

            var paymentPlan = CalculateAmericanMethodPaymentPlan(credit);
            request.OutputPort.Handle(paymentPlan);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error calculating payment plan: {ex.Message}");
        }
    }

    private List<decimal> CalculateAmericanMethodPaymentPlan(Credit credit)
    {
        var paymentPlan = new List<decimal>();
        var periodsPerYear = CalculatePeriodsPerYear(credit.Frequencies);
        var totalPeriods = credit.NumberOfYears * periodsPerYear;
        var initialFlow = CalculateInitialFlow(credit);
        paymentPlan.Add(initialFlow);
        var couponPayment = CalculateCouponPayment(credit);
        for (int i = 1; i < totalPeriods; i++)
        {
            paymentPlan.Add(couponPayment);
        }
        var finalPayment = CalculateFinalPayment(credit, couponPayment);
        paymentPlan.Add(finalPayment);

        return paymentPlan;
    }

    private decimal CalculateInitialFlow(Credit credit)
    {
        var initialFlow = -credit.ComercialValue;
        if (credit.FlotationRate.HasValue)
        {
            initialFlow -= credit.ComercialValue * (credit.FlotationRate.Value / 100);
        }
        if (credit.CavaliRate.HasValue)
        {
            initialFlow -= credit.ComercialValue * (credit.CavaliRate.Value / 100);
        }

        return initialFlow;
    }

    private decimal CalculateCouponPayment(Credit credit)
    {
        var tesRate = ConvertTeaToTes(credit);
        
        return credit.NominalValue * tesRate;
    }
    
    private decimal ConvertTeaToTes(Credit credit)
    {
        var tea = credit.CuponRate / 100; 
        var daysInPeriod = CalculateDaysInPeriod(credit.Frequencies, credit.DayPerYear);
        var daysInYear = credit.DayPerYear;
        
        var tes = Math.Pow(1 + (double)tea, (double)daysInPeriod / (double)daysInYear) - 1;
        return (decimal)tes;
    }
    
    private int CalculateDaysInPeriod(Frequencies frequency, int dayPerYear)
    {
        return frequency switch
        {
            Frequencies.Monthly => 30,
            Frequencies.Bimester => 60,
            Frequencies.Trimestre => 90,
            Frequencies.Quarterly => 90,
            Frequencies.Semestral => 180,
            Frequencies.Year => dayPerYear,
            Frequencies.FifteenDays => 15,
            _ => throw new ArgumentOutOfRangeException(nameof(frequency), "Invalid frequency type.")
        };
    }

    private decimal CalculateFinalPayment(Credit credit, decimal couponPayment)
    {
        
        var finalPayment = couponPayment + credit.NominalValue;
        if (credit.PrimRate.HasValue)
        {
            finalPayment += credit.NominalValue * (credit.PrimRate.Value / 100);
        }

        return finalPayment;
    }

    private int CalculatePeriodsPerYear(Frequencies frequency)
    {
        return frequency switch
        {
            Frequencies.Monthly => 12,
            Frequencies.Bimester => 6,
            Frequencies.Trimestre => 4,
            Frequencies.Quarterly => 4,
            Frequencies.Semestral => 2,
            Frequencies.Year => 1,
            Frequencies.FifteenDays => 24,
            _ => throw new ArgumentOutOfRangeException(nameof(frequency), "Invalid frequency type.")
        };
    }
    
    private decimal ConvertNominalToEffective(Credit credit)
    {
        var capitalizationPeriodsPerYear = CalculateCapitalizationPeriodsPerYear(credit.CapitalizationTypes);
        var nominalRate = credit.InterestRate / 100;
        
        var effectiveRate = Math.Pow(1 + (double)(nominalRate / capitalizationPeriodsPerYear), (double)capitalizationPeriodsPerYear) - 1;
        return (decimal)effectiveRate;
    }

    private int CalculateCapitalizationPeriodsPerYear(CapitalizationTypes capitalizationType)
    {
        return capitalizationType switch
        {
            CapitalizationTypes.Daily => 360,
            CapitalizationTypes.Weekly => 52,
            CapitalizationTypes.Monthly => 12,
            CapitalizationTypes.Bimonthly => 6,
            CapitalizationTypes.Cuatrimester => 3,
            CapitalizationTypes.Semester => 2,
            CapitalizationTypes.Annual => 1,
            _ => throw new ArgumentOutOfRangeException(nameof(capitalizationType), "Invalid capitalization type.")
        };
    }
    
    private decimal AdjustCouponRateToFrequency(Credit credit)
    {
        var periodsPerYear = CalculatePeriodsPerYear(credit.Frequencies);
        return credit.CuponRate / periodsPerYear;
    }
}