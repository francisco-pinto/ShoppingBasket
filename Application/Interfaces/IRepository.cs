namespace Application.Interfaces;

using Domain.Domain;
using Domain.SeedWork;

public interface IRepository<TEntity>
    where TEntity : EntityBase
{
    IUnitOfWork UnitOfWork { get; }
    
    Task<TEntity> AddAsync(TEntity entity);

    Task AddRangeAsync(IEnumerable<TEntity> entity);
    
    Task<List<TEntity>> GetAllAsync();
    
    Task<TEntity?> GetAsync(int id);
    
    Task<bool> Remove(TEntity entity);

    Task<TEntity> UpdateAsync(TEntity entity);
}