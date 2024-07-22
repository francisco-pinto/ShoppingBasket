using Application.Interfaces;
using Domain.Domain;

namespace Infrastructure.Repositories.DiscountRepository;

public class DiscountRepository : GenericRepository<Discount>, IDiscountRepository
{
    public DiscountRepository(ShoppingBasketDbContext context) : base(context)
    {
        
    }
}