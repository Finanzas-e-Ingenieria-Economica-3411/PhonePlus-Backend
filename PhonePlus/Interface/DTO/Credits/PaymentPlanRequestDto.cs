using PhonePlus.Domain.Enums;

namespace PhonePlus.Interface.DTO.Credits;

public class PaymentPlanRequestDto
{
    public int CreditId { get; set; }
    public decimal CokValue { get; set; }
    public InterestRates CokType { get; set; }
    public Frequencies CokFrequency { get; set; }
    public CapitalizationTypes? CokCapitalization { get; set; }
}

