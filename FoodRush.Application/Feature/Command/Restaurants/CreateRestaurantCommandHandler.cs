namespace FoodRush.Application.Feature.Command.Restaurants
{
    public record CreateRestaurantCommand(RestaurantDto RestaurantDto) : IRequest<ApiResponse<RestaurantDto>>;
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, ApiResponse<RestaurantDto>>
    {
        private readonly IUnitofwork _unitofwork;
        private readonly ILogger<CreateRestaurantCommand> _logger;
        public CreateRestaurantCommandHandler(IUnitofwork unitofwork, ILogger<CreateRestaurantCommand> logger)
        {
            _unitofwork = unitofwork;
            _logger = logger;
        }

        public async Task<ApiResponse<RestaurantDto>> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start Handling CreateRestaurantCommand For Restaurnt: {RestaurantName}",request.RestaurantDto.Name);

            var isExist = await _unitofwork.RestaurantRepository.IsExist(_=> _.Name.ToLower() == request.RestaurantDto.Name.ToLower());
            if (isExist)
            {
                _logger.LogWarning("Restaurant  already exists: {RestaurantName}",request.RestaurantDto.Name);
                return new ApiResponse<RestaurantDto>(HttpStatusCode.BadRequest, "This Restaurant Already Added");
            }

            var restaurant = new Restaurant
            {
                Name = request.RestaurantDto.Name,
                Address = request.RestaurantDto.Address,
                phoneNumber = request.RestaurantDto.phoneNumber,
                isOpen = request.RestaurantDto.isOpen,
                Rating = request.RestaurantDto.Rating,
            };

            await _unitofwork.RestaurantRepository.CreateAsync(restaurant);

            _logger.LogInformation("Meal created successfully: {RestaurantName}",request.RestaurantDto.Name);

            return new ApiResponse<RestaurantDto>(HttpStatusCode.OK,"Success Create Opration",request.RestaurantDto);
        }
    }


}
