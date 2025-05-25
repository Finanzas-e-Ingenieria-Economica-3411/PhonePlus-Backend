using PhonePlus.Common.Repository;
using PhonePlus.Domain.Models;

namespace PhonePlus.Domain.Repositories;

public interface IStateRepository : IBaseRepository<State>
{
    Task<bool> IsStateExistsAsync(string type);
    Task<bool> IsExistsByIdAsync(int id);
}