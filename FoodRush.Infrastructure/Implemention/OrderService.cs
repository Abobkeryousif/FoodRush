

namespace FoodRush.Infrastructure.Implemention
{
    public class OrderService : IOrderService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly ApplicationDbContaxt _contaxt;
        private readonly IMapper _mapper;
        public OrderService(IUnitofwork unitofwork, ApplicationDbContaxt contaxt, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _contaxt = contaxt;
            _mapper = mapper;
        }

        public async Task<Orders> CreateOrderAsync(OrderDto orderDto, string buyerEmail)
        {
            var basket = await _unitofwork.BasketRepository.GetBasketAsync(orderDto.basketId);

            if (basket == null)
                throw new Exception($"Basket with id '{orderDto.basketId}' not found in Redis.");

            if (basket.basketItems == null || !basket.basketItems.Any())
                throw new Exception("Basket is empty.");

            List<OrderItem> orderItems = new();

            foreach (var item in basket.basketItems)
            {
                var meal = await _unitofwork.MealRepository.GetByIdAsync(item.Id);
                if (meal == null)
                    throw new Exception($"Meal with id {item.Id} not found.");

                var orderItem = new OrderItem(meal.mealId, meal.Name, item.Price, item.Quantity);
                orderItems.Add(orderItem);
            }

            var deliveryMethod = await _contaxt.Deliveries
                .FirstOrDefaultAsync(d => d.Id == orderDto.deliveryId);
            if (deliveryMethod == null)
                throw new Exception($"Delivery method with id {orderDto.deliveryId} not found.");

            var subTotal = orderItems.Sum(p => p.Price * p.Quantity);
            var shippingAddress = _mapper.Map<ShippingAddress>(orderDto.ShippingAddress)
                                  ?? throw new Exception("Invalid shipping address.");

            var order = new Orders(buyerEmail, subTotal, shippingAddress, deliveryMethod, orderItems,basket.paymentIntentId);

            await _contaxt.Orders.AddAsync(order);
            await _contaxt.SaveChangesAsync();

            await _unitofwork.BasketRepository.DeleteBasketAsync(orderDto.basketId);
            return order;
        }



        public async Task<IReadOnlyList<Delivery>> GetAllDeliveryMethodAsync()=>
            await _contaxt.Deliveries.AsNoTracking().ToListAsync();
        

        public async Task<IReadOnlyList<Orders>> GetAllUserOrdersAsync(string bauyerEmail)
        {
            var orders = await _contaxt.Orders.Where(o=> o.bauyerEmail == bauyerEmail)
                .Include(inc => inc.orderItems)
                    .Include(inc => inc.Delivery).ToListAsync();

            return orders;
        }

        public async Task<Orders> GetOrderByIdAsync(int id, string bauyerEmail)
        {
            var order = await _contaxt.Orders.Where(o=> o.Id == id && o.bauyerEmail == bauyerEmail)
                .Include(inc=> inc.orderItems)
                    .Include(inc=> inc.Delivery)
                        .FirstOrDefaultAsync();

            if (order == null)
                throw new Exception($"Order With ID: {id} Not Found");

            return order;
        }
    }
}
