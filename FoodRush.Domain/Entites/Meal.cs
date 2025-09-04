namespace FoodRush.Domain.Entites
{

    public class Meal
    {
        public int mealId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int RestaurantId { get; set; }

        // Navigation properties
        public List<Photo> Photos { get; set; } = new();
        public Restaurant Restaurant { get; set; }
    }
}
