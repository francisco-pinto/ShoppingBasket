namespace Domain.Domain;

public abstract class EntityBase
{
    public int Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? ModifiedAt { get; set; }
}