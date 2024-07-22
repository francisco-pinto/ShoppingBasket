using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Tests.AggregateModels;

[TestClass]
public class Item
{
    [TestMethod]
    public void ApplyDiscount_ToItem_WithSuccess()
    {
        //Arrange
        var product = new Domain.Product();
        var quantity = 1;
        var priceInCentsByUnity = 50;
        var createdAt = new DateTime(2024, 08, 01);
        var modifiedAt = new DateTime(2024, 09, 01);
        
        var item = Domain.Item.Create(
            product,
            quantity,
            priceInCentsByUnity,
            createdAt,
            null);
        
        //Act
        item.ApplyDiscount(modifiedAt);

        //Assert
        Assert.IsNull(item.Discount);
        Assert.IsNotNull(item.Product);
        
        Assert.AreEqual(modifiedAt, item.ModifiedAt);
        Assert.AreEqual(priceInCentsByUnity, item.PriceInCentsByUnity);
        Assert.AreEqual(quantity, item.Quantity);
        Assert.AreEqual(createdAt, item.CreatedAt);
    }
}