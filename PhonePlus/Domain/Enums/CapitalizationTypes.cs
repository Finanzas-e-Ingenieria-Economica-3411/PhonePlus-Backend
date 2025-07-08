using System.ComponentModel;

namespace PhonePlus.Domain.Enums;

public enum CapitalizationTypes
{
    [Description("Cuatrimestral")]
    Cuatrimester = 1,
    [Description("Semestral")]
    Semester = 2,
    [Description("Anual")]
    Annual = 3,
    [Description("Bimestral")]
    Bimonthly = 4,
    [Description("Mensual")]
    Monthly = 5,
    [Description("Semanal")]
    Weekly = 6,
    [Description("Diario")]
    Daily = 7,
}