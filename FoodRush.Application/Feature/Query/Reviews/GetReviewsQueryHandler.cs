
namespace FoodRush.Application.Feature.Query.Reviews
{
    public record GetReviewsQuery(int restaurantId) : IRequest<ApiResponse<List<GetRestaurantReviews>>>;
    public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, ApiResponse<List<GetRestaurantReviews>>>
    {
        private readonly IUnitofwork _unitofwork;

        public GetReviewsQueryHandler(IUnitofwork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task<ApiResponse<List<GetRestaurantReviews>>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _unitofwork.ReviewRepository.GetReviewsByRestaurantIdAsync(request.restaurantId);

            if (reviews == null || !reviews.Any())
                return new ApiResponse<List<GetRestaurantReviews>>(HttpStatusCode.NotFound,$"Not Found Any Reviews");

            var result = reviews.Select(r => new GetRestaurantReviews
            {
                Comment = r.Comment,
                Rating = r.Rating,
                restaurant = r.Restaurant.Name,

            }).ToList();

            return new ApiResponse<List<GetRestaurantReviews>>(HttpStatusCode.OK, "Reviews Retrieved Successfully",result);
        }
    }
}
