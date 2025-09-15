

namespace FoodRush.Infrastructure.Implemention
{
    public class ReviewRepository : MinimalRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContaxt context) : base(context)
        {
        }
    }
}
