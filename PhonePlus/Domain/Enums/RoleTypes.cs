using System.ComponentModel;

namespace PhonePlus.Domain.Enums;

public enum RoleTypes
{
   [Description("Emisor")]
   Emisor = 1,
   [Description("Inversionista")]
   Inversor = 2,
}