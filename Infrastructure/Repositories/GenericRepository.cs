using Application.Interfaces;
using Domain.Domain;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenericRepository<Entity>: IRepository<Entity>
    where Entity: EntityBase
{
    public IUnitOfWork UnitOfWork => this.Context;
    
    protected readonly ShoppingBasketDbContext Context;
    
    protected readonly DbSet<Entity> Entities;

    public GenericRepository(ShoppingBasketDbContext context)
    {
        this.Context = context;

        this.Entities = context.Set<Entity>();
    }
    
    public async Task<Entity> AddAsync(Entity entity)
    {
        var entityEntry = await this.Entities.AddAsync(entity);

        return entityEntry.Entity;
    }
    
    public async Task AddRangeAsync(IEnumerable<Entity> entity)
    {
        await this.Entities.AddRangeAsync(entity);
    }

    public async Task<List<Entity>> GetAllAsync()
    {
        return await this.Entities.ToListAsync();
    }

    public async Task<Entity?> GetAsync(int id)
    {
        return await this.Entities.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<bool> Remove(Entity entity)
    {
        await Task.FromResult(this.Entities.Remove(entity));
        return true;
    }

    public async Task<Entity> UpdateAsync(Entity entity)
    {
        var dataEntity = await Task.FromResult(this.Entities.Update(entity));

        return dataEntity.Entity;
    }
}