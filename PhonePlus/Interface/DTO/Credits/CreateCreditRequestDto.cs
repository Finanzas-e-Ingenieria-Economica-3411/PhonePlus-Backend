using PhonePlus.Domain.Enums;

namespace PhonePlus.Interface.DTO.Credits;



public record CreateCreditRequestDto(
    decimal ComercialValue,
    decimal NominalValue,
    decimal? StructurationRate,
    decimal? ColonRate,
    decimal? FlotationRate,
    decimal? CavaliRate,
    decimal? PrimRate,
    int NumberOfYears,
    Frequencies Frequencies,
    int DayPerYear,
    CapitalizationTypes CapitalizationTypes,
    decimal CuponRate,
    InterestRates CuponRateType,
    Frequencies CuponRateFrequency,
    CapitalizationTypes? CuponRateCapitalization,
    CurrencyTypes Currency,
    List<GracePeriodDto> GracePeriods,
    int UserId
);