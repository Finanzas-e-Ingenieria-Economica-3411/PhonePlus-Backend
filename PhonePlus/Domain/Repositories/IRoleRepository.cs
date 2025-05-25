using PhonePlus.Common.Repository;
using PhonePlus.Domain.Models;

namespace PhonePlus.Domain.Repositories;

public interface IRoleRepository : IBaseRepository<UserRole>
{
    Task<bool> IsRoleExists(string roleName);
    Task<string?> GetRoleName(int roleId);
}