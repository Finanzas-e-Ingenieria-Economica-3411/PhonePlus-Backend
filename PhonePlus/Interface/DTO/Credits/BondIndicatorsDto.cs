namespace PhonePlus.Interface.DTO.Credits;

public class BondIndicatorsDto
{
    public decimal TCEA { get; set; }
    public decimal TREA { get; set; }
    public decimal Duration { get; set; }
    public decimal ModifiedDuration { get; set; }
    public decimal Convexity { get; set; }
    public decimal MaxMarketPrice { get; set; }
    public List<decimal> CashFlows { get; set; } = new();
}

