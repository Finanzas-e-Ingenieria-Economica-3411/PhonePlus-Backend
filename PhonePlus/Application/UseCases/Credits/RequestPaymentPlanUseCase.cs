using MediatR;
using PhonePlus.Application.Ports.Credits.Input;
using PhonePlus.Domain.Enums;
using PhonePlus.Domain.Models;
using PhonePlus.Domain.Repositories;
using PhonePlus.Interface.DTO.Credits;

namespace PhonePlus.Application.UseCases.Credits;

public sealed class RequestPaymentPlanUseCase(ICreditRepository creditRepository) : IRequestHandler<RequestPaymentPlanInputPort>
{
    public async Task Handle(RequestPaymentPlanInputPort request, CancellationToken cancellationToken)
    {
        try
        {
            var dto = request.RequestData;
            var credit = await creditRepository.GetCreditByIdAsync(dto.CreditId);
            if (credit == null)
            {
                throw new Exception("Credit not found.");
            }

            // Convertir el COK a tasa efectiva del periodo de pago del bono
            var cokEffective = ConvertCokToEffectivePerPeriod(
                dto.CokValue,
                dto.CokType,
                dto.CokFrequency,
                dto.CokCapitalization,
                credit.Frequencies,
                credit.DayPerYear
            );

            var paymentPlan = CalculateAmericanMethodPaymentPlan(credit);
            var indicators = CalculateIndicators(credit, paymentPlan, cokEffective);
            request.OutputPort.Handle(indicators);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error calculating payment plan: {ex.Message}");
        }
    }

    // Conversión de COK a tasa efectiva del periodo de pago del bono
    private decimal ConvertCokToEffectivePerPeriod(decimal value, InterestRates type, Frequencies cokFreq, CapitalizationTypes? capitalization, Frequencies bondFreq, int dayPerYear)
    {
        int daysCok = CalculateDaysInPeriod(cokFreq, dayPerYear);
        int daysBond = CalculateDaysInPeriod(bondFreq, dayPerYear);
        double rate = (double)value / 100.0; // Asegura que la tasa esté en decimal
        if (type == InterestRates.Effective)
        {
            double effCok = Math.Pow(1 + rate, (double)daysBond / daysCok) - 1;
            return (decimal)effCok;
        }
        else // Nominal
        {
            if (capitalization == null)
                throw new Exception("Capitalization type is required for nominal rate");
            int capPeriods = CalculateCapitalizationPeriodsPerYear(capitalization.Value);
            double nom = rate;
            double effAnnual = Math.Pow(1 + nom / capPeriods, capPeriods) - 1;
            double effBond = Math.Pow(1 + effAnnual, (double)daysBond / dayPerYear) - 1;
            return (decimal)effBond;
        }
    }

    private List<decimal> CalculateAmericanMethodPaymentPlan(Credit credit)
    {
        var paymentPlan = new List<decimal>();
        var periodsPerYear = CalculatePeriodsPerYear(credit.Frequencies);
        var totalPeriods = credit.NumberOfYears * periodsPerYear;
        var initialFlow = CalculateInitialFlow(credit);
        paymentPlan.Add(initialFlow);
        var tesRate = ConvertCuponToEffectivePerPeriod(
            credit.CuponRate,
            credit.CuponRateType,
            credit.CuponRateFrequency,
            credit.CuponRateCapitalization,
            credit.Frequencies,
            credit.DayPerYear
        );
        //TODO: corregir el cálculo de conversión de tasas, agregar tasa de descuento y conversión de tasas a efectivo por periodo de pago del bono
        
        // Detectar si se debe usar nominal ajustado
        bool usarNominalAjustado = credit.CuponRateType == InterestRates.Nominal &&
            (credit.CuponRateCapitalization != null && CalculatePeriodsPerYear(credit.Frequencies) != CalculateCapitalizationPeriodsPerYear(credit.CuponRateCapitalization.Value));
        decimal baseNominal = usarNominalAjustado ? credit.NominalValue * (1 + tesRate) : credit.NominalValue;
        var couponPayment = baseNominal * tesRate;
        // Crear un diccionario para acceso rápido a los períodos de gracia
        var graceDict = credit.GracePeriods.ToDictionary(g => g.Period, g => g.Type);
        for (int i = 1; i < totalPeriods; i++)
        {
            if (graceDict.TryGetValue(i, out var graceType))
            {
                if (graceType == GraceType.Total)
                {
                    paymentPlan.Add(0);
                }
                else if (graceType == GraceType.Parcial)
                {
                    paymentPlan.Add(couponPayment);
                }
                else // Ninguno
                {
                    paymentPlan.Add(couponPayment);
                }
            }
            else
            {
                paymentPlan.Add(couponPayment);
            }
        }
        var finalPayment = baseNominal * tesRate;
        if (credit.PrimRate.HasValue)
        {
            finalPayment += baseNominal * (credit.PrimRate.Value / 100);
        }
        finalPayment += baseNominal;
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
        var tesRate = ConvertCuponToEffectivePerPeriod(
            credit.CuponRate,
            credit.CuponRateType,
            credit.CuponRateFrequency,
            credit.CuponRateCapitalization,
            credit.Frequencies,
            credit.DayPerYear
        );
        bool usarNominalAjustado = credit.CuponRateType == InterestRates.Nominal &&
            (credit.CuponRateCapitalization != null && CalculatePeriodsPerYear(credit.Frequencies) != CalculateCapitalizationPeriodsPerYear(credit.CuponRateCapitalization.Value));
        decimal baseNominal = usarNominalAjustado ? credit.NominalValue * (1 + tesRate) : credit.NominalValue;
        return baseNominal * tesRate;
    }

    // Conversión de tasa cupón a efectiva del periodo de pago del bono
    private decimal ConvertCuponToEffectivePerPeriod(decimal value, InterestRates type, Frequencies cuponFreq, CapitalizationTypes? capitalization, Frequencies bondFreq, int dayPerYear)
    {
        int daysCupon = CalculateDaysInPeriod(cuponFreq, dayPerYear);
        int daysBond = CalculateDaysInPeriod(bondFreq, dayPerYear);
        double rate = (double)value / 100.0; // Asegura que la tasa esté en decimal
        if (type == InterestRates.Effective)
        {
            double effCupon = Math.Pow(1 + rate, (double)daysBond / daysCupon) - 1;
            return (decimal)effCupon;
        }
        else // Nominal
        {
            if (capitalization == null)
                throw new Exception("Capitalization type is required for nominal coupon rate");
            int capPeriods = CalculateCapitalizationPeriodsPerYear(capitalization.Value);
            double nom = rate;
            double effAnnual = Math.Pow(1 + nom / capPeriods, capPeriods) - 1;
            double effBond = Math.Pow(1 + effAnnual, (double)daysBond / dayPerYear) - 1;
            return (decimal)effBond;
        }
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
        var tesRate = ConvertCuponToEffectivePerPeriod(
            credit.CuponRate,
            credit.CuponRateType,
            credit.CuponRateFrequency,
            credit.CuponRateCapitalization,
            credit.Frequencies,
            credit.DayPerYear
        );
        bool usarNominalAjustado = credit.CuponRateType == InterestRates.Nominal &&
            (credit.CuponRateCapitalization != null && CalculatePeriodsPerYear(credit.Frequencies) != CalculateCapitalizationPeriodsPerYear(credit.CuponRateCapitalization.Value));
        decimal baseNominal = usarNominalAjustado ? credit.NominalValue * (1 + tesRate) : credit.NominalValue;
        var finalPayment = baseNominal * tesRate;
        if (credit.PrimRate.HasValue)
        {
            finalPayment += baseNominal * (credit.PrimRate.Value / 100);
        }
        finalPayment += baseNominal;
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
        var nominalRate = credit.CuponRate / 100;
        
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

    // Calcula los indicadores financieros requeridos
    private BondIndicatorsDto CalculateIndicators(Credit credit, List<decimal> cashFlows, decimal cok)
    {
        // Separar el flujo inicial (inversión) de los flujos futuros
        decimal initialInvestment = cashFlows[0]; // Negativo
        var futureCashFlows = cashFlows.Skip(1).ToList();

        // Precio de mercado (valor presente de los flujos futuros descontados a COK)
        decimal price = PresentValue(futureCashFlows, cok);
        
        // Duración y convexidad (solo sobre los flujos futuros)
        decimal duration = Duration(futureCashFlows, cok, price);
        decimal modifiedDuration = duration / (1 + cok);
        decimal convexity = Convexity(futureCashFlows, cok, price);

        // TCEA (emisor): TIR de los flujos completos
        decimal tcea = CalculateIRR(cashFlows);
        // TREA (inversionista): TIR de los flujos invertidos (cambiar signo)
        decimal trea = CalculateIRR(cashFlows.Select(f => -f).ToList());

        return new BondIndicatorsDto
        {
            TCEA = tcea,
            TREA = trea,
            Duration = duration,
            ModifiedDuration = modifiedDuration,
            Convexity = convexity,
            MaxMarketPrice = price,
            CashFlows = cashFlows
        };
    }

    // Calcula la TIR (IRR) de una serie de flujos
    private decimal CalculateIRR(List<decimal> cashFlows, int maxIterations = 100, decimal guess = 0.1m)
    {
        decimal x0 = guess;
        for (int iter = 0; iter < maxIterations; iter++)
        {
            decimal f = 0;
            decimal df = 0;
            for (int t = 0; t < cashFlows.Count; t++)
            {
                decimal denom = (decimal)Math.Pow(1 + (double)x0, t);
                f += cashFlows[t] / denom;
                if (t > 0)
                    df -= t * cashFlows[t] / (decimal)Math.Pow(1 + (double)x0, t + 1);
            }
            if (Math.Abs((double)f) < 1e-7)
                return x0;
            if (df == 0) break;
            x0 = x0 - f / df;
        }
        return x0;
    }

    // Valor presente de los flujos descontados a la tasa COK (solo flujos futuros)
    private decimal PresentValue(List<decimal> cashFlows, decimal cok)
    {
        decimal pv = 0;
        for (int t = 1; t <= cashFlows.Count; t++)
        {
            pv += cashFlows[t - 1] / (decimal)Math.Pow(1 + (double)cok, t);
        }
        return pv;
    }

    // Duración de Macaulay (solo flujos futuros)
    private decimal Duration(List<decimal> cashFlows, decimal cok, decimal price)
    {
        decimal sum = 0;
        for (int t = 1; t <= cashFlows.Count; t++)
        {
            sum += t * cashFlows[t - 1] / (decimal)Math.Pow(1 + (double)cok, t);
        }
        return price == 0 ? 0 : sum / price;
    }

    // Convexidad (solo flujos futuros)
    private decimal Convexity(List<decimal> cashFlows, decimal cok, decimal price)
    {
        decimal sum = 0;
        for (int t = 1; t <= cashFlows.Count; t++)
        {
            sum += cashFlows[t - 1] * t * (t + 1) / (decimal)Math.Pow(1 + (double)cok, t + 2);
        }
        return price == 0 ? 0 : sum / price;
    }
}