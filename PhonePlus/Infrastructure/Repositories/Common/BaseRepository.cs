using Microsoft.EntityFrameworkCore;
using PhonePlus.Common.Repository;
using PhonePlus.Infrastructure.Context;

namespace PhonePlus.Infrastructure.Repositories.Common;

public class BaseRepository<TEntity>(AppDbContext context) : IBaseRepository<TEntity> where TEntity : class
{
    public async Task<bool> AddAsync(TEntity entity)
    {
        await context.Set<TEntity>().AddAsync(entity);
        return true;
    }

    public async Task<TEntity?> FindByIdAsync(int id)
    {
        var entity = await context.Set<TEntity>().FindAsync(id);
        return entity;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var entities = await context.Set<TEntity>().ToListAsync();
        return entities;
    }

    public void Update(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }
}