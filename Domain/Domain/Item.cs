using Domain.Domain.DiscountTypes;

namespace Domain.Domain;

public class Item : EntityBase
{
    public Item()
    {
    }

    private Item(
        Product product, 
        int quantity, 
        int priceInCentsByUnity, 
        DateTime createdAt,
        Discount? discount)
    {
        this.Discount = discount;
        this.PriceInCentsByUnity = priceInCentsByUnity;
        this.Product = product;
        this.Quantity = quantity;
        this.DiscountIsApplied = false;
        this.DiscountPriceByUnity = 0;
        this.CreatedAt = createdAt;
    }
    
    public Product Product { get; private set; }
    public int Quantity { get; private set; }
    public int DiscountPriceByUnity { get; private set; }
    public int TotalDiscountPriceInCents => DiscountPriceByUnity * Quantity;
    public int PriceInCentsByUnity { get; private set; }
    public int TotalPriceInCents => PriceInCentsByUnity * Quantity;
    public Discount? Discount { get; private set; }
    public bool DiscountIsApplied { get; private set; } = false;

    public void AddDiscount(Discount discount, DateTime modifiedAt)
    {
        this.Discount = discount;

        if (discount is QuantityDiscount)
        {
            return;
        }

        var percentageDiscount = (PercentageDiscount)discount;
        
        decimal discountPrice = (decimal)this.TotalPriceInCents * (1m - (decimal)percentageDiscount!.Percentage / 100m);

        this.DiscountPriceByUnity = Convert.ToInt32(discountPrice);
        
        this.ModifiedAt = modifiedAt;
    }
    
    public void ApplyDiscount(DateTime modifiedAt)
    {
        this.DiscountIsApplied = true;
        this.ModifiedAt = modifiedAt;
    }

    public static Item Create(
        Product product, 
        int quantity, 
        int priceInCentsByUnity,
        DateTime createdAt,
        Discount? discount)
    {
        return new(
            product,
            quantity,
            priceInCentsByUnity,
            createdAt,
            null);
    }
}
