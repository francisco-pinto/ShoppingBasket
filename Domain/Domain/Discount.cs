namespace Domain.Domain;

using DiscountTypes;

public abstract class Discount : EntityBase
{
    public DateTime BeginDate { get; protected init; }
    public DateTime EndDate { get; protected init; }
    public abstract DiscountType DiscountType { get; protected set;  }
}