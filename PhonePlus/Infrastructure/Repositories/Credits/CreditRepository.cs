using Microsoft.EntityFrameworkCore;
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

    public async Task<IEnumerable<Credit>> GetCreditsByStateId(int stateId)
    {
        var credits = await context.Set<Credit>()
            .Where(c => c.StateId == stateId)
            .ToListAsync();
        return credits;
    }

    public async Task<IEnumerable<Credit>> GetAvailableCredits()
    {
        
        var credits = await context.Set<Credit>()
            .Where(c => c.StateId == 1 && c.StartDate.AddMonths(c.Months) > DateTime.Now)
            .ToListAsync();
        return credits;
    }
}