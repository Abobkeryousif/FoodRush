

namespace FoodRush.Application.Feature.Command.Restaurants
{
    public record DeleteRestaurantCommand(int id) : IRequest<ApiResponse<string>>;
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand, ApiResponse<string>>
    {
        private readonly IUnitofwork _unitofwork;

        public DeleteRestaurantCommandHandler(IUnitofwork unitofwork)=>
            _unitofwork = unitofwork;
        
        public async Task<ApiResponse<string>> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _unitofwork.RestaurantRepository.GetByIdAsync(request.id);
            if (restaurant == null)
                return new ApiResponse<string>(HttpStatusCode.NotFound,$"Not Found With ID: {request.id}");

            await _unitofwork.RestaurantRepository.DeleteAsync(restaurant);
            return new ApiResponse<string>(HttpStatusCode.OK,"Success",$"Deleted Restaurant: {restaurant.Name}");
        }
    }
}

