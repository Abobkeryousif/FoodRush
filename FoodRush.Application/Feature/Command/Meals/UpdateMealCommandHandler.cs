

namespace FoodRush.Application.Feature.Command.Meals
{
    public record UpdateMealCommand(int id,updateMealDto mealDto) : IRequest<ApiResponse<updateMealDto>>;
    public class UpdateMealCommandHandler : IRequestHandler<UpdateMealCommand, ApiResponse<updateMealDto>>
    {
        private readonly IUnitofwork _unitofwork;

        public UpdateMealCommandHandler(IUnitofwork unitofwork)=>
            _unitofwork = unitofwork;
        
        public async Task<ApiResponse<updateMealDto>> Handle(UpdateMealCommand request, CancellationToken cancellationToken)
        {
            var meal = await _unitofwork.MealRepository.GetByIdAsync(request.id);
            if (meal == null)
                return new ApiResponse<updateMealDto>(HttpStatusCode.NotFound , $"Not Found With Id: {request.id}");

            var isExist = await _unitofwork.MealRepository.IsExist(m=> m.Name.ToLower() == request.mealDto.Name.ToLower() 
            && m.RestaurantId == request.mealDto.RestaurantId);
            
            if (isExist)
            {
                return new ApiResponse<updateMealDto>(HttpStatusCode.BadGateway,$"This Meal Already Added!");
            }
                
            meal.Name = request.mealDto.Name;
            meal.Description = request.mealDto.Description;
            meal.Price = request.mealDto.Price;
            meal.IsAvailable = request.mealDto.IsAvailable;

            await _unitofwork.MealRepository.UpdateAsync(meal);
            return new ApiResponse<updateMealDto>(HttpStatusCode.OK,"Success",request.mealDto);
        }
    }
}

