
namespace FoodRush.Application.DTOs
{
    public class ReviewDto
    {
        public int RestaurantId { get; set; }
        public string? Comment { get; set; }
        public decimal Rating { get; set; }
    }

    public record GetAndUpdateReviewDto
    {
        public string? Comment { get; set; }
        public decimal Rating { get; set; }
    }

    public record GetRestaurantReviews : GetAndUpdateReviewDto
    {
        public string? restaurant { get; set; }
    }

}
