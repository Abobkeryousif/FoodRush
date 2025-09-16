

namespace FoodRush.Application.Feature.Command.Reviews
{
    public record UpdateReviewCommand(int reviewId , GetAndUpdateReviewDto reviewDto) : IRequest<ApiResponse<GetAndUpdateReviewDto>>;
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, ApiResponse<GetAndUpdateReviewDto>>
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;

        public UpdateReviewCommandHandler(IUnitofwork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetAndUpdateReviewDto>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _unitofwork.ReviewRepository.FirstOrDefaultAsync(r=> r.Id == request.reviewId);
            if (review == null)
                return new ApiResponse<GetAndUpdateReviewDto>(HttpStatusCode.NotFound, $"Review with ID {request.reviewId} not found");

            review.Comment = request.reviewDto.Comment;
            review.Rating = request.reviewDto.Rating;
            review.createOn = DateTime.Now;

            await _unitofwork.ReviewRepository.UpdateAsync(review);


            //To Get Old Reviews And Add Updateed Review With Calc Avargre For All Rating😊
            var restaurant = await _unitofwork.RestaurantRepository
            .FirstOrDefaultAsync(r => r.Id == review.restaurantId, include: new[] { "Reviews" });



            if (restaurant != null && restaurant.Reviews.Any())
            {
                restaurant.Rating = (double)Math.Round(restaurant.Reviews.Average(r=> r.Rating),1);
                await _unitofwork.RestaurantRepository.UpdateAsync(restaurant);
            }

            var result = _mapper.Map<GetAndUpdateReviewDto>(review);

            return new ApiResponse<GetAndUpdateReviewDto>(HttpStatusCode.OK, "Review updated successfully", result);


        }
    }
}
