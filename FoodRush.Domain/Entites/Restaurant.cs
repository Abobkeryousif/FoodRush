
namespace FoodRush.Domain.Entites
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string phoneNumber { get; set; }
        public bool isOpen { get; set; } = true;
        public double? Rating { get; set; }

        // Navigation property
        public List<Meal> Meals { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();
    }
}
