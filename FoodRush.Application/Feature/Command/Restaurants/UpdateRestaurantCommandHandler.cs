
namespace FoodRush.Application.Feature.Command.Restaurants
{
    public record UpdateRestaurantCommand(int id,RestaurantDto restaurantDto) : IRequest<ApiResponse<RestaurantDto>>;
    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, ApiResponse<RestaurantDto>>
    {
        private readonly IUnitofwork _unitofwork;
        public UpdateRestaurantCommandHandler(IUnitofwork unitofwork)=>
            _unitofwork = unitofwork;
        
        public async Task<ApiResponse<RestaurantDto>> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _unitofwork.RestaurantRepository.FirstOrDefaultAsync(r=> r.Id == request.id);
            if (restaurant == null)
                return new ApiResponse<RestaurantDto>(HttpStatusCode.NotFound,$"Not Found With ID: {request.id}");

            restaurant.Name =  request.restaurantDto.Name;
            restaurant.Address = request.restaurantDto.Address;
            restaurant.phoneNumber = request.restaurantDto.phoneNumber;
            restaurant.isOpen = request.restaurantDto.isOpen;

            await _unitofwork.RestaurantRepository.UpdateAsync(restaurant);
            return new ApiResponse<RestaurantDto>(HttpStatusCode.OK,"Success",request.restaurantDto);
        }
    }
}
