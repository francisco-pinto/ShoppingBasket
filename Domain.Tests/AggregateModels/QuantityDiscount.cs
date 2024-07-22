namespace Domain.Tests.AggregateModels;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class QuantityDiscount
{
    [TestMethod]
    public void Create_QuantityDiscount_WithSuccess()
    {
        //Arrange
        var beginDate = new DateTime(2024, 07, 01);
        var endDate = new DateTime(2024, 08, 01);

        var itemDiscounts = new List<Domain.DiscountTypes.QuantityDiscount.ItemDiscount>()
        {
            new (1, 50),
            new (2, 25),
            new (3, 20)
        };
        
        var expectedDiscount = new Domain.DiscountTypes.QuantityDiscount(
            beginDate, 
            endDate, 
            itemDiscounts);
        
        //Act
        var actualDiscount = Domain.DiscountTypes.QuantityDiscount.Create(
            beginDate, 
            endDate, 
            itemDiscounts);

        //Assert
        Assert.IsNull(actualDiscount.ModifiedAt);
        
        Assert.AreEqual(expectedDiscount.BeginDate, actualDiscount.BeginDate);
        Assert.AreEqual(expectedDiscount.EndDate, actualDiscount.EndDate);
        Assert.AreEqual(expectedDiscount.CreatedAt, actualDiscount.CreatedAt);
    }
}