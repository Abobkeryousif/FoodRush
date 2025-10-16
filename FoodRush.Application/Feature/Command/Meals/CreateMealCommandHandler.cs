
namespace FoodRush.Application.Feature.Command.Meals
{
    public record CreateMealCommand(AddMealDto mealDto) : IRequest<ApiResponse<string>>;
    public class CreateMealCommandHandler : IRequestHandler<CreateMealCommand, ApiResponse<string>>
    {
        private readonly IUnitofwork _unitofwork;
        public CreateMealCommandHandler(IUnitofwork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public async Task<ApiResponse<string>> Handle(CreateMealCommand request, CancellationToken cancellationToken)
        {

            var isExist = await _unitofwork.MealRepository.IsExist(m=> m.Name.ToLower() == request.mealDto.Name.ToLower() && m.RestaurantId == request.mealDto.RestaurantId);
            if (isExist)
            {
                return new ApiResponse<string>(HttpStatusCode.BadRequest, "This Meal Already Added");
            }

            await _unitofwork.MealRepository.AddAsync(request.mealDto);

            return new ApiResponse<string>(HttpStatusCode.OK,"Success",$"Added Meal : {request.mealDto.Name}");

        }
    }
}
