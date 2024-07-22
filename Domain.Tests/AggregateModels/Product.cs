using System.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Tests.AggregateModels;

[TestClass]
public class Product
{
    [TestMethod]
    public void Create_Product_WithSuccess()
    {
        //Arrange
        var name = "Bread";
        var date = new DateTime(2024, 08, 01);

        var expectedProduct = new Domain.Product(name, date);
        
        //Act
        var actual = Domain.Product.Create(name, date);

        //Assert
        Assert.IsNull(expectedProduct.ModifiedAt);
        
        Assert.AreEqual(expectedProduct.Name, actual.Name);
        Assert.AreEqual(expectedProduct.CreatedAt, actual.CreatedAt);
    }
}