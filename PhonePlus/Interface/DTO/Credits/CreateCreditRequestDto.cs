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
    InterestRates InterestRates,
    CapitalizationTypes CapitalizationTypes,
    decimal InterestRate,
    decimal YearDiscount,
    decimal RentImport,
    int UserId,
    decimal CuponRate,
    CurrencyTypes Currency
);