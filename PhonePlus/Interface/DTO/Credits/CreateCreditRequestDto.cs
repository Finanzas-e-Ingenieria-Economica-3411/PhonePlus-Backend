namespace PhonePlus.Interface.DTO.Credits;

public record CreateCreditRequestDto(
    int PhoneNumber,
    decimal Price,
    DateTime StartDate,
    int Months,
    decimal InterestRate,
    int Insurance,
    int Amortization,
    int Paid,
    int Interest,
    int PendingPayment,
    int UserId
);