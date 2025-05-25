using Microsoft.EntityFrameworkCore;
using PhonePlus.Domain.Models;
using PhonePlus.Domain.Repositories;
using PhonePlus.Infrastructure.Context;
using PhonePlus.Infrastructure.Repositories.Common;

namespace PhonePlus.Infrastructure.Repositories.Users;

public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<bool> AlreadyExists(string email)
    {
        return await context.Set<User>().AnyAsync(u => u.Email == email);
    }

    public async Task<bool> AlreadyExists(string dni, string username)
    {
        return await context.Set<User>().AnyAsync(u => u.Dni == dni && u.Username == username);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await context.Set<User>()
            .FirstOrDefaultAsync(u => u.Email == email);
    }
    
}