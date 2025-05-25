using System.ComponentModel;

namespace PhonePlus.Domain.Enums;

public enum States
{
    [Description("Pending")]
    Pending = 1,
    [Description("Approve")]
    Approve = 2,
    [Description("Reject")]
    Reject = 3
}