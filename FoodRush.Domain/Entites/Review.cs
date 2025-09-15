
namespace FoodRush.Domain.Entites
{
    public class Review
    {
        public int Id { get; set; }
        public int restaurantId { get; set; }
        public int userId { get; set; }
        public string? Comment { get; set; }

        [Range(1,5)]
        public double Rating { get; set; }
        public DateTime createOn { get; set; } = DateTime.Now;

        public Restaurant Restaurant { get; set; }  
        public User User { get; set; }

    }
}
