
namespace FoodRush.Application.Feature.Query.Meals
{
    public record GetMealByIdQuery(int id) : IRequest<ApiResponse<Meal>>;
    public class GetMealByIdQueryHandler : IRequestHandler<GetMealByIdQuery, ApiResponse<Meal>>
    {
        private readonly IUnitofwork _unitofwork;

        public GetMealByIdQueryHandler(IUnitofwork unitofwork)=>
            _unitofwork = unitofwork;
        

        public async Task<ApiResponse<Meal>> Handle(GetMealByIdQuery request, CancellationToken cancellationToken)
        {
            var meal = await _unitofwork.MealRepository.GetByIdAsync(request.id);
            if (meal == null)
                return new ApiResponse<Meal>(HttpStatusCode.NotFound,$"Not Found With ID: {request.id}");

            return new ApiResponse<Meal>(HttpStatusCode.OK,"Success",meal);
        }
    }
}
