

namespace FoodRush.Application.Feature.Command.Meals
{
    public record DeleteMealCommand(int id) : IRequest<ApiResponse<string>>;
    public class DeleteMealCommandHandler : IRequestHandler<DeleteMealCommand, ApiResponse<string>>
    {
        private readonly IUnitofwork _unitofwork;
        public DeleteMealCommandHandler(IUnitofwork unitofwork)=>
            _unitofwork = unitofwork;
        

        public async Task<ApiResponse<string>> Handle(DeleteMealCommand request, CancellationToken cancellationToken)
        {
            var meal = await _unitofwork.MealRepository.GetByIdAsync(request.id);
            if (meal == null)
                return new ApiResponse<string>(HttpStatusCode.NotFound,$"Not Found With ID: {request.id}");

            await _unitofwork.MealRepository.DeleteAsync(meal);

            return new ApiResponse<string>(HttpStatusCode.OK,"Success",$"Deleted Meal : {meal.Name}");
        }
    }
}
