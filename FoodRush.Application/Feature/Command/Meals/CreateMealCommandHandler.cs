
namespace FoodRush.Application.Feature.Command.Meals
{
    public record CreateMealCommand(AddMealDto mealDto) : IRequest<ApiResponse<string>>;
    public class CreateMealCommandHandler : IRequestHandler<CreateMealCommand, ApiResponse<string>>
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;
        private ILogger<CreateMealCommandHandler> _logger;
        public CreateMealCommandHandler(IUnitofwork unitofwork, IMapper mapper, ILogger<CreateMealCommandHandler> logger)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponse<string>> Handle(CreateMealCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start Handling CreateMealCommand For Meal : {MealName}, RestaurantId: {RestaurantId}",
                request.mealDto.Name , request.mealDto.RestaurantId);

            var isExist = await _unitofwork.MealRepository.IsExist(m=> m.Name.ToLower() == request.mealDto.Name.ToLower() && m.RestaurantId == request.mealDto.RestaurantId);
            if (isExist)
            {
                _logger.LogWarning("Meal already exists: {MealName}, RestaurantId: {RestaurantId}",
                    request.mealDto.Name, request.mealDto.RestaurantId);

                return new ApiResponse<string>(HttpStatusCode.BadRequest, "This Meal Already Added");
            }

            await _unitofwork.MealRepository.AddAsync(request.mealDto);

            _logger.LogInformation("Meal created successfully: {MealName}",
                request.mealDto.Name);


            return new ApiResponse<string>(HttpStatusCode.OK,"Success",$"Added Meal : {request.mealDto.Name}");

        }
    }
}
