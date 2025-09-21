

namespace FoodRush.Application.Feature.Command.Meals
{
    public record PatchMealAvailabilityCommand(int id, PatchMealAvailabilityDto mealDto) : IRequest<ApiResponse<updateMealDto>>;
    public class PatchMealAvailabilityCommandHandler : IRequestHandler<PatchMealAvailabilityCommand, ApiResponse<updateMealDto>>
    {
        private readonly IUnitofwork _unitofwork;

        public PatchMealAvailabilityCommandHandler(IUnitofwork unitofwork)=>
            _unitofwork = unitofwork;
        

        public async Task<ApiResponse<updateMealDto>> Handle(PatchMealAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var meal = await _unitofwork.MealRepository.GetByIdAsync(request.id);
            if (meal == null)
                return new ApiResponse<updateMealDto>(HttpStatusCode.NotFound,$"Not Found With ID: {request.id}");

            meal.IsAvailable = request.mealDto.IsAvailable;
            await _unitofwork.MealRepository.UpdateAsync(meal);

            return new ApiResponse<updateMealDto>(HttpStatusCode.OK,"Success",new updateMealDto
            {
                Name = meal.Name,
                Description = meal.Description,
                Price = meal.Price,
                IsAvailable = request.mealDto.IsAvailable
            });
        }
    }
}
