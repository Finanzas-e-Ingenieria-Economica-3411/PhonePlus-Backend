using System.ComponentModel;

namespace PhonePlus.Domain.Enums;

public enum Frequencies
{
    [Description("Bimestral")]
    Bimester = 1,
    [Description("Trimestral")]
    Trimestre = 2,
    [Description("Anual")]
    Year = 3,
    [Description("Quincenal")]
    FifteenDays = 4,
    [Description("Mensual")]
    Monthly = 5,
    [Description("Cuatrimestral")]
    Quarterly = 6,
    [Description("Semestral")]
    Semestral = 7
    
    
}