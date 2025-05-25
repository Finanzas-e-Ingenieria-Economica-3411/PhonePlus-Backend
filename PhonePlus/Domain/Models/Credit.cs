using PhonePlus.Interface.DTO.Credits;

namespace PhonePlus.Domain.Models;

public sealed class Credit
{
    public int Id { get; private set; }
    public int PhoneNumber { get; private set; }
    public decimal Price { get; private set; }
    public DateTime StartDate { get; private set; }
    public int Months { get; private set; }
    public decimal InterestRate { get; private set; }
    public int Insurance { get; private set; }
    public int Amortization { get; private set; }
    public int Paid { get; private set; }
    public int Interest { get; private set; }
    public int PendingPayment { get; private set; }
    public int UserId { get; private set; }
    public int StateId { get; private set; }

    public Credit()
    {
        PhoneNumber = 0;
        Price = 0;
        StartDate = DateTime.MinValue;
        Months = 0;
        InterestRate = 0;
        Insurance = 0;
        Amortization = 0;
        Paid = 0;
        Interest = 0;
        PendingPayment = 0;
        UserId = 0;
        StateId = 1;
    }
    
    public Credit(CreateCreditRequestDto dto)
    {
        PhoneNumber = dto.PhoneNumber;
        Price = dto.Price;
        StartDate = dto.StartDate;
        Months = dto.Months;
        InterestRate = dto.InterestRate;
        Insurance = dto.Insurance;
        Amortization = dto.Amortization;
        Paid = dto.Paid;
        Interest = dto.Interest;
        PendingPayment = dto.PendingPayment;
        UserId = dto.UserId;
        StateId = 1;
    }
    
    public void UpdateState(int newStateId)
    {
        StateId = newStateId;
    }
}
