using PhonePlus.Domain.Enums;
using PhonePlus.Interface.DTO.Credits;

namespace PhonePlus.Domain.Models;

public sealed class Credit
{
    public int Id { get; private set; }
    public decimal ComercialValue { get; private set; }
    public decimal NominalValue { get; private set; }
    public int NumberOfYears { get; private set; }
    public States State { get; private set; }
    public Frequencies Frequencies { get; private set; }
    public int DayPerYear { get; private set; }
    public CapitalizationTypes CapitalizationTypes { get; private set; }
    public decimal? StructurationRate { get; private set; }
    public decimal? ColonRate { get; private set; }
    public decimal? FlotationRate { get; private set; }
    public decimal? CavaliRate { get; private set; }
    public decimal? PrimRate { get; private set; }
    public DateTime EmitionDate { get; private set; }
    public int UserId { get; private set; }
    public decimal CuponRate { get; private set; }
    public InterestRates CuponRateType { get; private set; }
    public Frequencies CuponRateFrequency { get; private set; }
    public CapitalizationTypes? CuponRateCapitalization { get; private set; }
    public CurrencyTypes Currency { get; private set; }
    public List<GracePeriod> GracePeriods { get; private set; } = new();

    public Credit()
    {
        EmitionDate = DateTime.Now;
    }

    public Credit(CreateCreditRequestDto request)
    {
        ComercialValue = request.ComercialValue;
        NominalValue = request.NominalValue;
        StructurationRate = request.StructurationRate;
        ColonRate = request.ColonRate;
        FlotationRate = request.FlotationRate;
        CavaliRate = request.CavaliRate;
        PrimRate = request.PrimRate;
        NumberOfYears = request.NumberOfYears;
        State = States.Registered;
        Frequencies = request.Frequencies;
        DayPerYear = request.DayPerYear;
        CapitalizationTypes = request.CapitalizationTypes;
        UserId = request.UserId;
        CuponRate = request.CuponRate;
        CuponRateType = request.CuponRateType;
        CuponRateFrequency = request.CuponRateFrequency;
        CuponRateCapitalization = request.CuponRateCapitalization;
        Currency = request.Currency;
        EmitionDate = DateTime.Now;
        GracePeriods = request.GracePeriods?.Select(g => new GracePeriod { Period = g.Period, Type = g.Type }).ToList() ?? new List<GracePeriod>();
    }

    public void UpdateState(States state)
    {
        State = state;
    }
}
