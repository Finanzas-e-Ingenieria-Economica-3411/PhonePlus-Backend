using PhonePlus.Domain.Enums;

namespace PhonePlus.Interface.DTO.Credits;


public record UpdateCreditRequestDto(
    int Id,
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
    decimal YearDiscount,
    decimal RentImport,
    int UserId,
    decimal CuponRate,
    InterestRates CuponRateType,
    Frequencies CuponRateFrequency,
    CapitalizationTypes? CuponRateCapitalization,
    CurrencyTypes Currency,
    List<GracePeriodDto> GracePeriods
);
