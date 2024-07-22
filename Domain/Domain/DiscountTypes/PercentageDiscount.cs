namespace Domain.Domain.DiscountTypes;

public class PercentageDiscount : Discount
{
    public PercentageDiscount(
        DateTime beginDate,
        DateTime endDate,
        int percentage,
        DateTime createdAt)
    {
        this.Percentage = percentage;
        this.CreatedAt = createdAt;
        this.BeginDate = beginDate;
        this.EndDate = endDate;
    }
    public override DiscountType DiscountType { get; protected set; } = DiscountType.PercentageDiscount;
    public int Percentage { get; private init; }

    public static Discount Create(
        DateTime beginDate,
        DateTime endDate,
        int percentage,
        DateTime createdAt)
    {
        return new PercentageDiscount(
            beginDate,
            endDate,
            percentage,
            createdAt);
    }
}