
namespace FoodRush.Application.Contract.Interface
{
    public interface IMealRepository : IGeneraicRepository<Meal>
    {
        Task<List<GetMealDto>> GetAllMealAsyncV1();
        Task<List<GetMealDto>> GetAllMealAsyncV2(QueryParameters parameters);
        Task<bool> AddAsync(AddMealDto mealDto);

    }
}
