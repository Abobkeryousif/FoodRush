
namespace FoodRush.Domain.Entites.Order
{
    public class OrderItem
    {
        public OrderItem()
        {
            
        }
        public OrderItem(int Mealid, string MealName, decimal price, int quantity)
        {
            mealId = Mealid;
            mealName  = MealName;
            Price = price;
            Quantity = quantity;
        }
        public int Id { get; set; }
        public int mealId { get; set; }
        public string mealName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
