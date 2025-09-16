
namespace FoodRush.Application.Contract.Interface
{
    public interface IReviewRepository : IMinimalRepository<Review>
    {
        Task<List<Review>> GetReviewsByRestaurantIdAsync(int restaurantId);
        bool DeleteReview(Review review);

    }
}
