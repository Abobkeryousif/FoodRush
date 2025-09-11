
namespace FoodRush.Application.Contract.Interface
{
    public interface IBasketRepository
    {
        Task<List<Basket>> GetAllBasketAsync();
        Task<Basket> GetBasketAsync(string id);
        Task<Basket> UpdateBasketAsync(Basket basket);
        Task<bool> DeleteBasketAsync(string id);
    }
}
