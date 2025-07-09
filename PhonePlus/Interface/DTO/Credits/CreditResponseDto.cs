using PhonePlus.Domain.Enums;
using System.Collections.Generic;

namespace PhonePlus.Interface.DTO.Credits;

public record CreditResponseDto(
    int Id,
    decimal ComercialValue,
    decimal NominalValue,
    decimal? StructurationRate,
    decimal? ColonRate,
    decimal? FlotationRate,
    decimal? CavaliRate,
    decimal? PrimRate,
    int NumberOfYears,
    States State,
    Frequencies Frequencies,
    int DayPerYear,
    CapitalizationTypes CapitalizationTypes,
    decimal CuponRate,
    InterestRates CuponRateType,
    Frequencies CuponRateFrequency,
    CapitalizationTypes? CuponRateCapitalization,
    CurrencyTypes Currency,
    string ClientName,
    string Username,
    List<GracePeriodDto> GracePeriods
    );