using PhonePlus.Common.Repository;
using PhonePlus.Domain.Models;

namespace PhonePlus.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<bool> AlreadyExists(string email);
    Task<bool> AlreadyExists(string dni, string username);
    
    Task<User?> GetUserByEmailAsync(string email);
}