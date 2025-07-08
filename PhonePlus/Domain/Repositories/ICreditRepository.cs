using PhonePlus.Common.Repository;
using PhonePlus.Domain.Models;

namespace PhonePlus.Domain.Repositories;

public interface ICreditRepository : IBaseRepository<Credit>
{
    Task<IEnumerable<Credit>> GetCreditsByUserIdAsync(int userId);
    
    Task<Credit?> GetCreditByIdAsync(int creditId);

    Task<IEnumerable<Credit>> GetAvailableCredits();

}