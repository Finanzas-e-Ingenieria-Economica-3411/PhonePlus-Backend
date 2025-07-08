using PhonePlus.Domain.Enums;

namespace PhonePlus.Domain.Models;

public class GracePeriod
{
    public int Id { get; set; }
    public int Period { get; set; }
    public GraceType Type { get; set; }
    public int CreditId { get; set; }
    public Credit Credit { get; set; } = null!;
}
