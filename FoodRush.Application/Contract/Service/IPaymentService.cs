
namespace FoodRush.Application.Contract.Service
{
    public interface IPaymentService
    {
        Task<Basket> CreateOrUpdatePaymentAsync(string basketId, int? deliveryId);
    }
}
