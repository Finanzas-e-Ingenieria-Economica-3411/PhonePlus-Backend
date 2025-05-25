using PhonePlus.Common.Repository;
using PhonePlus.Infrastructure.Context;

namespace PhonePlus.Infrastructure.Repositories.Common;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}