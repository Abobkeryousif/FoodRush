
namespace FoodRush.Domain.Entites.Order
{
    public class Orders
    {
        public Orders()
        {
            
        }
        public Orders(string BauyerEmail, decimal SubTotal, ShippingAddress ShippingAddress, Delivery delivery, IReadOnlyList<OrderItem> OrderItems, string paymentIntentId)
        {
            bauyerEmail = BauyerEmail;
            subTotal = SubTotal;
            shippingAddress = ShippingAddress;
            Delivery = delivery;
            orderItems = OrderItems;
            this.paymentIntentId = paymentIntentId;
        }
        public Orders(string BauyerEmail, decimal SubTotal, ShippingAddress ShippingAddress, Delivery delivery, IReadOnlyList<OrderItem> OrderItems)
        {
            bauyerEmail = BauyerEmail;
            subTotal = SubTotal;
            shippingAddress = ShippingAddress;
            Delivery = delivery;
            orderItems = OrderItems;
            
        }

        public int Id { get; set; }
        public string bauyerEmail { get; set; }
        public decimal subTotal { get; set; } 
        public DateTime orderDate { get; set; } = DateTime.Now;
        public ShippingAddress shippingAddress { get; set; }
        public string paymentIntentId { get; set; }
        public Delivery Delivery { get; set; }
        public IReadOnlyList<OrderItem> orderItems { get; set; }
        public Status status { get; set; } = Status.Pending;

        public decimal GetTotal()
        {
            return subTotal + Delivery.Price;
        }

    }
}
