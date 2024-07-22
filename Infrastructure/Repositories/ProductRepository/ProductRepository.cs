using Application.Interfaces;
using Domain.Domain;

namespace Infrastructure.Repositories.ProductRepository;

public class ProductRepository(ShoppingBasketDbContext context)
    : GenericRepository<Product>(context), IProductRepository;