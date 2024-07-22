namespace Domain.Domain.Basket;

public class Basket : EntityBase
{
    public List<Item> Items { get; private set; } = new();
    public PurchaseStatus PurchaseStatus { get; private set;  }
    public int TotalPriceInCents => CalculatePriceWithoutDiscounts();
    public int TotalPriceWithDiscountsInCents => CalculatePriceWithDiscounts();
    private int CalculatePriceWithoutDiscounts() 
        => Items.Sum(item => item.TotalPriceInCents);
    
    private int CalculatePriceWithDiscounts() 
        => Items.Sum(item => item.TotalDiscountPriceInCents);

    public void Buy()
    {
        PurchaseStatus = PurchaseStatus.Purchased;
        
        //TODO: Domain Event to create the invoice
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    public static Basket Create(DateTime now)
    {
        return new Basket
        {
            PurchaseStatus = PurchaseStatus.Active,
            Items = new(),
            CreatedAt = now
        };
    }
}