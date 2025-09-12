
namespace FoodRush.Application.Contract.Service
{
    public interface IOrderService
    {
        Task<Orders> CreateOrderAsync(OrderDto orderDto, string bauyerEmail);
        Task<IReadOnlyList<Orders>> GetAllUserOrdersAsync(string bauyerEmail);
        Task<Orders> GetOrderByIdAsync(int id, string bauyerEmail);
        Task<IReadOnlyList<Delivery>> GetAllDeliveryMethodAsync();
    }
}
