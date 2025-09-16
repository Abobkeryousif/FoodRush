
namespace FoodRush.Domain.Entites
{
    public class Review
    {
        public int Id { get; set; }
        public int restaurantId { get; set; }
        public int userId { get; set; }
        public string? Comment { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public decimal Rating { get; set; }
        public DateTime createOn { get; set; } = DateTime.Now;

        public Restaurant Restaurant { get; set; }  
        public User User { get; set; }

    }
}
