

namespace FoodRush.Application.Feature.Command.Reviews
{
    public record CreateReviewCommand(ReviewDto review,int userId) : IRequest<ApiResponse<GetAndUpdateReviewDto>>;
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ApiResponse<GetAndUpdateReviewDto>>
    {
        private readonly IUnitofwork _unitofwork;

        public CreateReviewCommandHandler(IUnitofwork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task<ApiResponse<GetAndUpdateReviewDto>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var restaurant= await _unitofwork.RestaurantRepository.FirstOrDefaultAsync(r=> r.Id == request.review.RestaurantId);

            if (restaurant == null)
                return new ApiResponse<GetAndUpdateReviewDto>(HttpStatusCode.NotFound,$"Not Found With ID: {request.review.RestaurantId}");

            var review = new Review
            {
                restaurantId = request.review.RestaurantId,
                userId = request.userId,
                Comment = request.review.Comment,
                Rating = request.review.Rating,
                createOn = DateTime.Now,
            };

            await _unitofwork.ReviewRepository.CreateAsync(review);

            restaurant.Rating = (double)Math.Round(restaurant.Reviews.Average(r=> r.Rating),1);
            await _unitofwork.RestaurantRepository.UpdateAsync(restaurant);

            var resultDto = new GetAndUpdateReviewDto
            {
                Comment = review.Comment,
                Rating = review.Rating
            };

            return new ApiResponse<GetAndUpdateReviewDto>(HttpStatusCode.Created, "Thank You For Sharing Your Experience.😊", resultDto);
        }
    }
}
