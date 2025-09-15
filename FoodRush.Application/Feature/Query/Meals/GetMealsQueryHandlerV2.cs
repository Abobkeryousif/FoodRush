
namespace FoodRush.Application.Feature.Query.Meals
{
    public record GetMealsQueryV2(QueryParameters parameters) : IRequest<ApiResponse<List<GetMealDto>>>;
    public class GetMealsQueryHandlerV2 : IRequestHandler<GetMealsQueryV2, ApiResponse<List<GetMealDto>>>
    {
        private readonly IUnitofwork _unitofwork;

        public GetMealsQueryHandlerV2(IUnitofwork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task<ApiResponse<List<GetMealDto>>> Handle(GetMealsQueryV2 request, CancellationToken cancellationToken)
        {
            var meals = await _unitofwork.MealRepository.GetAllMealAsyncV2(request.parameters);
            if (meals.Count == 0)
                return new ApiResponse<List<GetMealDto>>(HttpStatusCode.NotFound, "Not Founed Any Meals");

            return new ApiResponse<List<GetMealDto>>(HttpStatusCode.OK, "Success", meals);
        }
    }
}
