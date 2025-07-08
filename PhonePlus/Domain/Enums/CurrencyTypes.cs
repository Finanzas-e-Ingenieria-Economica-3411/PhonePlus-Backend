using System.ComponentModel;

namespace PhonePlus.Domain.Enums;

public enum CurrencyTypes
{
    [Description("Soles")]
    Soles = 1,
    [Description("Dolares")]
    Dollars = 2,
    [Description("Euros")]
    Euros = 3,
}