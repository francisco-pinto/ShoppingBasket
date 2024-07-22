using Application.Interfaces;
using Domain.Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.ItemRepository;

public class ItemRepository(ShoppingBasketDbContext context) : GenericRepository<Item>(context), IItemRepository
{
}