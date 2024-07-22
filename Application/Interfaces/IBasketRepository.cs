namespace Application.Interfaces;

using Domain.Domain.Basket;

public interface IBasketRepository: IRepository<Basket>
{
    Task<Basket?> GetBasketAsync(int id);
    Task<List<Basket>> GetAllBaskets();
    Task<Basket?> GetBasketWithId(int id);
}
