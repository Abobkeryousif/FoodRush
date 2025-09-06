namespace FoodRush.Application.Feature.Command.Restaurants
{
    public record CreateRestaurantCommand(RestaurantDto RestaurantDto) : IRequest<ApiResponse<RestaurantDto>>;
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, ApiResponse<RestaurantDto>>
    {
        private readonly IUnitofwork _unitofwork;

        public CreateRestaurantCommandHandler(IUnitofwork unitofwork)=>
            _unitofwork = unitofwork;
        
        public async Task<ApiResponse<RestaurantDto>> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var isExist = await _unitofwork.RestaurantRepository.IsExist(_=> _.Name.ToLower() == request.RestaurantDto.Name.ToLower());
            if (isExist)    
                return new ApiResponse<RestaurantDto>(HttpStatusCode.BadRequest,"This Restaurant Already Added");

            var restaurant = new Restaurant
            {
                Name = request.RestaurantDto.Name,
                Address = request.RestaurantDto.Address,
                phoneNumber = request.RestaurantDto.phoneNumber,
                isOpen = request.RestaurantDto.isOpen,
                Rating = request.RestaurantDto.Rating,
            };

            await _unitofwork.RestaurantRepository.CreateAsync(restaurant);
            return new ApiResponse<RestaurantDto>(HttpStatusCode.OK,"Success Create Opration",request.RestaurantDto);
        }
    }


}
