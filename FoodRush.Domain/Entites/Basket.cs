
namespace FoodRush.Domain.Entites
{
    public class Basket
    {
        public Basket()
        {
            
        }

        public Basket(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
        public string paymentIntentId { get; set; }
        public string clientSecret { get; set; }
        public List<BasketItem> basketItems { get; set; } = new List<BasketItem>();
    }
}
