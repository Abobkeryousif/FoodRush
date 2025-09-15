

namespace FoodRush.Application.Feature.Query.Meals
{
    public record GetMealsQuery : IRequest<ApiResponse<List<GetMealDto>>>;
    public class GetMealsQueryHandler : IRequestHandler<GetMealsQuery, ApiResponse<List<GetMealDto>>>
    {
        private readonly IUnitofwork _unitofwork;
        public GetMealsQueryHandler(IUnitofwork unitofwork)=>
            _unitofwork = unitofwork;
        
        public async Task<ApiResponse<List<GetMealDto>>> Handle(GetMealsQuery request, CancellationToken cancellationToken)
        {
            var meals = await _unitofwork.MealRepository.GetAllMealAsyncV1();
            if (meals.Count == 0)
                return new ApiResponse<List<GetMealDto>>(HttpStatusCode.NotFound,"Not Founed Any Meals");

            return new ApiResponse<List<GetMealDto>>(HttpStatusCode.OK,"Success",meals);
        }
    }
}
