using Microsoft.Extensions.Logging;

namespace FoodRush.Infrastructure.Implemention
{
    public class OrderService : IOrderService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly ApplicationDbContaxt _contaxt;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IUnitofwork unitofwork, ApplicationDbContaxt contaxt, IMapper mapper, ILogger<OrderService> logger)
        {
            _unitofwork = unitofwork;
            _contaxt = contaxt;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Orders> CreateOrderAsync(OrderDto orderDto, string buyerEmail)
        {
            _logger.LogInformation("Creating order for BuyerEmail: {BuyerEmail}, BasketId: {BasketId}", buyerEmail, orderDto.basketId);

            var basket = await _unitofwork.BasketRepository.GetBasketAsync(orderDto.basketId);

            if (basket == null)
            {
                _logger.LogWarning("Basket not found: {BasketId}", orderDto.basketId);
                throw new Exception($"Basket with id '{orderDto.basketId}' not found in Redis.");
            }

            if (basket.basketItems == null || !basket.basketItems.Any())
            {
                _logger.LogWarning("Basket is empty: {BasketId}", orderDto.basketId);
                throw new Exception("Basket is empty.");
            }

            List<OrderItem> orderItems = new();

            foreach (var item in basket.basketItems)
            {
                var meal = await _unitofwork.MealRepository.GetByIdAsync(item.Id);
                if (meal == null)
                {
                    _logger.LogWarning("Meal not found: {MealId}", item.Id);
                    throw new Exception($"Meal with id {item.Id} not found.");
                }

                var orderItem = new OrderItem(meal.mealId, meal.Name, item.Price, item.Quantity);
                orderItems.Add(orderItem);
            }

            var deliveryMethod = await _contaxt.Deliveries.FirstOrDefaultAsync(d => d.Id == orderDto.deliveryId);
            if (deliveryMethod == null)
            {
                _logger.LogWarning("Delivery method not found: {DeliveryId}", orderDto.deliveryId);
                throw new Exception($"Delivery method with id {orderDto.deliveryId} not found.");
            }

            var subTotal = orderItems.Sum(p => p.Price * p.Quantity);
            var shippingAddress = _mapper.Map<ShippingAddress>(orderDto.ShippingAddress)
                                  ?? throw new Exception("Invalid shipping address.");

            var order = new Orders(buyerEmail, subTotal, shippingAddress, deliveryMethod, orderItems, basket.paymentIntentId);

            await _contaxt.Orders.AddAsync(order);
            await _contaxt.SaveChangesAsync();

            await _unitofwork.BasketRepository.DeleteBasketAsync(orderDto.basketId);

            _logger.LogInformation("Order created successfully for BuyerEmail: {BuyerEmail}, OrderId: {OrderId}", buyerEmail, order.Id);

            return order;
        }

        public async Task<IReadOnlyList<Delivery>> GetAllDeliveryMethodAsync()
        {
            _logger.LogInformation("Fetching all delivery methods");
            return await _contaxt.Deliveries.AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<Orders>> GetAllUserOrdersAsync(string buyerEmail)
        {
            _logger.LogInformation("Fetching all orders for BuyerEmail: {BuyerEmail}", buyerEmail);
            var orders = await _contaxt.Orders
                .Where(o => o.bauyerEmail == buyerEmail)
                .Include(o => o.orderItems)
                .Include(o => o.Delivery)
                .ToListAsync();

            return orders;
        }

        public async Task<Orders> GetOrderByIdAsync(int id, string buyerEmail)
        {
            _logger.LogInformation("Fetching order by Id: {OrderId} for BuyerEmail: {BuyerEmail}", id, buyerEmail);
            var order = await _contaxt.Orders
                .Where(o => o.Id == id && o.bauyerEmail == buyerEmail)
                .Include(o => o.orderItems)
                .Include(o => o.Delivery)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                _logger.LogWarning("Order not found: {OrderId} for BuyerEmail: {BuyerEmail}", id, buyerEmail);
                throw new Exception($"Order With ID: {id} Not Found");
            }

            return order;
        }
    }
}
