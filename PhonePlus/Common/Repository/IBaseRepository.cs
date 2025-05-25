namespace PhonePlus.Common.Repository;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<bool> AddAsync(TEntity entity);
    Task<TEntity?> FindByIdAsync (int id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    void Update(TEntity entity);
    void Delete(TEntity entity);
}