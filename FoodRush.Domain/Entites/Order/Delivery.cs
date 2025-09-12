
namespace FoodRush.Domain.Entites.Order
{
    public class Delivery
    {
        public Delivery()
        {

        }
        public Delivery(string companyName, string description, string deliveryTime, decimal price)
        {

            CompanyName = companyName;
            Description = description;
            DeliveryTime = deliveryTime;
            Price = price;
        }
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }

        public string DeliveryTime { get; set; }
        public decimal Price { get; set; }
    }
}
