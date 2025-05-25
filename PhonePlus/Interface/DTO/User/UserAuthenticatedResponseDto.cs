namespace PhonePlus.Interface.DTO.User;

public record UserAuthenticatedResponseDto(int Id,string Email,string Token,string Name, string UserName, string Dni,string Role);