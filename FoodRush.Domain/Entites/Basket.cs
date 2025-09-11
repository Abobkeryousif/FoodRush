
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
        public List<BasketItem> basketItems { get; set; } = new List<BasketItem>();
    }
}
