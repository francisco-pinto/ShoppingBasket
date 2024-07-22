namespace Domain.Domain;

public class Product: EntityBase
{
    public Product()
    {
    }
    public Product(
        string name,
        DateTime createdAt)
    {
        this.Name = name;
        this.CreatedAt = createdAt;
    }
    public string Name { get; private set; }

    public static Product Create(
        string name,
        DateTime createdAt)
    {
        return new Product(name, createdAt);
    }
}