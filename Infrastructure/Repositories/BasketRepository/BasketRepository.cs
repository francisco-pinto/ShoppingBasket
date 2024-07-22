using Application.Interfaces;
using Domain.Domain.Basket;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.BasketRepository;
public class BasketRepository: GenericRepository<Basket>, IBasketRepository
{
    public BasketRepository(ShoppingBasketDbContext context) : base(context)
    {
        
    }

    public async Task<Basket?> GetBasketAsync(int id)
    {
        return await this.Context.Baskets
            .Where(x => x.Id == id)
            .Include(x => x.Items)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Basket>> GetAllBaskets()
    {
        return this.Context.Baskets.Include(x => x.Items).ToList();
    }

    public async Task<Basket?> GetBasketWithId(int id)
    {
        return this.Context.Baskets
            .Where(x => x.Id == id)
            .Include(x => x.Items).FirstOrDefault();
    }
}