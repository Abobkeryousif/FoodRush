

namespace FoodRush.Infrastructure.Implemention
{
    public class ReviewRepository : MinimalRepository<Review>, IReviewRepository
    {
        private readonly ApplicationDbContaxt _contaxt;
        public ReviewRepository(ApplicationDbContaxt context) : base(context)
        {
            _contaxt = context; 
        }

        public bool DeleteReview(Review review)
        {
            var currentReview = _contaxt.Remove(review);
            return _contaxt.SaveChanges() > 0;
        }

        public async Task<List<Review>> GetReviewsByRestaurantIdAsync(int restaurantId)
        {
            return await _contaxt.Reviews.Where(r=> r.restaurantId == restaurantId)
                .Include(r=> r.Restaurant)
                .ToListAsync();
        }
    }
}
