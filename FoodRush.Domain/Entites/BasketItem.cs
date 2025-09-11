
namespace FoodRush.Domain.Entites
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Restaurant { get; set; }
    }
}
