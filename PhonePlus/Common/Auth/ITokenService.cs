using PhonePlus.Domain.Models;

namespace PhonePlus.Common.Auth;

public interface ITokenService
{
    Task<(string, string)> GenerateCredentials(User user);
    Task<int?> ValidateToken(string token);
}