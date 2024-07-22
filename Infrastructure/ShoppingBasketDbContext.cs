using Domain.Domain;
using Domain.Domain.Basket;
using Domain.Domain.DiscountTypes;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ShoppingBasketDbContext: DbContext, IUnitOfWork
{
    public ShoppingBasketDbContext(DbContextOptions<ShoppingBasketDbContext> options)
        : base(options)
    {
    }

    public DbSet<Basket> Baskets { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<QuantityDiscount> QuantityDiscounts { get; set; }
    public DbSet<PercentageDiscount> PercentageDiscounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Discount>()
            .HasDiscriminator<DiscountType>("DiscountType")
            .HasValue<QuantityDiscount>(DiscountType.QuantityDiscount)
            .HasValue<PercentageDiscount>(DiscountType.PercentageDiscount);
        
        modelBuilder.Entity<Basket>()
            .HasMany(b => b.Items)
            .WithOne()
            .HasForeignKey("BasketId");

        modelBuilder.Entity<Item>()
            .HasOne(i => i.Discount)
            .WithMany()
            .HasForeignKey("DiscountId");
        
        modelBuilder.Entity<Item>()
            .HasOne(i => i.Product)
            .WithMany()
            .HasForeignKey("ProductId");

        modelBuilder.Entity<QuantityDiscount.ItemDiscount>()
            .HasKey(id => new { id.ItemId, id.Percentage });

        modelBuilder.Entity<QuantityDiscount.ItemDiscount>()
            .HasOne<Item>()
            .WithMany()
            .HasForeignKey(id => id.ItemId);

        modelBuilder.Entity<QuantityDiscount>()
            .HasMany(qd => qd.AssociatedItemDiscounts)
            .WithOne()
            .HasForeignKey("DiscountId");
    }
    
    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await this.SaveChangesAsync(cancellationToken);

        return true;
    }
}