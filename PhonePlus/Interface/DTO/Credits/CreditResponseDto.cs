namespace PhonePlus.Interface.DTO.Credits;

public record CreditResponseDto(
    int Id,
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
    string ClientName,
    string Username,
    int StateId
    );