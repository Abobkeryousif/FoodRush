

namespace FoodRush.Application.Feature.Command.Reviews
{
    public record DeleteReviewCommand(int reviewId) : IRequest<ApiResponse<string>>;
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, ApiResponse<string>>
    {
        private readonly IUnitofwork _unitofwork;

        public DeleteReviewCommandHandler(IUnitofwork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task<ApiResponse<string>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _unitofwork.ReviewRepository.FirstOrDefaultAsync(r => r.Id == request.reviewId);
            if (review == null)
                return new ApiResponse<string>(HttpStatusCode.NotFound, $"Not Found With ID: {request.reviewId}");

            _unitofwork.ReviewRepository.DeleteReview(review);

            var restaurant = await _unitofwork.RestaurantRepository
                .FirstOrDefaultAsync(r => r.Id == review.restaurantId, include: new[] { "Reviews" });

            if (restaurant != null)
            {
                restaurant.Rating = restaurant.Reviews.Any()
                    ? (double)Math.Round(restaurant.Reviews.Average(r => r.Rating), 1)
                    : 0;

                await _unitofwork.RestaurantRepository.UpdateAsync(restaurant);
            }

            return new ApiResponse<string>(HttpStatusCode.OK, "Success", "Review deleted successfully");
        }

    }
}
