using System.ComponentModel;

namespace PhonePlus.Domain.Enums;

public enum States
{
    [Description("Registrado")]
    Registered = 1,
    [Description("Solicitado")]
    Requested = 2,
    [Description("Aprobado")]
    Approved = 3,
}