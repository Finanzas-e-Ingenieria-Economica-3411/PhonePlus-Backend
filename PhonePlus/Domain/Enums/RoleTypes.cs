using System.ComponentModel;

namespace PhonePlus.Domain.Enums;

public enum RoleTypes
{
   [Description("Banned")]
   Banned = 1,
   [Description("Buyer")]
   Buyer = 2,
   [Description("Seller")]
   Seller = 3,
   [Description("Administrator")]
   Admin = 4,
}