using Microsoft.EntityFrameworkCore;
using PhonePlus.Domain.Models;
using PhonePlus.Domain.Repositories;
using PhonePlus.Infrastructure.Context;
using PhonePlus.Infrastructure.Repositories.Common;

namespace PhonePlus.Infrastructure.Repositories.Credits;

public sealed class StateRepository(AppDbContext context) : BaseRepository<State>(context), IStateRepository
{
    public async Task<bool> IsStateExistsAsync(string type)
    {
        return  await context.Set<State>().AnyAsync(x => x.Type == type);
    }

    public async Task<bool> IsExistsByIdAsync(int id)
    {
        return await context.Set<State>().AnyAsync(x => x.Id == id);
    }
}