using Microsoft.EntityFrameworkCore;
using PhonePlus.Common.Repository;
using PhonePlus.Domain.Models;
using PhonePlus.Domain.Repositories;
using PhonePlus.Infrastructure.Context;
using PhonePlus.Infrastructure.Repositories.Common;

namespace PhonePlus.Infrastructure.Repositories.Role;

public sealed class RoleRepository(AppDbContext context) : BaseRepository<UserRole>(context), IRoleRepository
{
    public async Task<bool> IsRoleExists(string roleName)
    {
        return await context.Set<UserRole>()
            .AnyAsync(x => x.Type == roleName);
    }

    public async Task<string?> GetRoleName(int roleId)
    {
        return await context.Set<UserRole>()
            .Where(x => x.Id == roleId)
            .Select(x => x.Type)
            .FirstOrDefaultAsync();
    }
}