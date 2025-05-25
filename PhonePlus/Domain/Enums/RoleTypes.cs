using System.ComponentModel;

namespace PhonePlus.Domain.Enums;

public enum RoleTypes
{
   [Description("Banned")]
   Banned = 1,
   [Description("Client")]
   User = 2,
   [Description("Administrator")]
   Admin = 3,
}