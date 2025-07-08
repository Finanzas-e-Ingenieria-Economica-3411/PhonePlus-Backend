using System.ComponentModel;

namespace PhonePlus.Domain.Enums;

public enum InterestRates
{
    [Description("Tasa Efectiva")]
    Effective = 1,
    [Description("Tasa Nominal")]
    Nominal = 2,
}