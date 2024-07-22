using System.Runtime.CompilerServices;

namespace Domain.Domain.DiscountTypes;

public class QuantityDiscount : Discount
{
    public QuantityDiscount()
    {
        
    }
    public QuantityDiscount(
        DateTime beginDate,
        DateTime endDate,
        List<ItemDiscount> associatedItemDiscounts)
    {
        this.EndDate = endDate;
        this.BeginDate = beginDate;
        AssociatedItemDiscounts = associatedItemDiscounts;
    }
    public override DiscountType DiscountType { get; protected set; } = DiscountType.QuantityDiscount;
    public List<ItemDiscount> AssociatedItemDiscounts { get; private set; } = [];

    public static QuantityDiscount Create(
        DateTime beginDate,
        DateTime endDate,
        List<ItemDiscount> associatedItemDiscounts)
    {
        return new QuantityDiscount(
            beginDate, 
            endDate, 
            associatedItemDiscounts);
    }

public record ItemDiscount(int ItemId, int Percentage);
}