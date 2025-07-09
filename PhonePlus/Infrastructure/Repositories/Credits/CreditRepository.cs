using Microsoft.EntityFrameworkCore;
using PhonePlus.Domain.Enums;
using PhonePlus.Domain.Models;
using PhonePlus.Domain.Repositories;
using PhonePlus.Infrastructure.Context;
using PhonePlus.Infrastructure.Repositories.Common;

namespace PhonePlus.Infrastructure.Repositories.Credits;

public sealed class CreditRepository(AppDbContext context) : BaseRepository<Credit>(context), ICreditRepository
{
    public async Task<IEnumerable<Credit>> GetCreditsByUserIdAsync(int userId)
    {
        var credits = await context.Set<Credit>()
            .Where(c => c.UserId == userId)
            .ToListAsync();
        return credits;
    }

    public async Task<Credit?> GetCreditByIdAsync(int creditId)
    {
        var credit = await context.Set<Credit>()
            .Where(c => c.Id == creditId).FirstOrDefaultAsync();
        return credit;
    }


    public async Task<IEnumerable<Credit>> GetAvailableCredits()
    {
        var credits = await context.Set<Credit>()
            .ToListAsync();
        return credits;
    }
}