
namespace FoodRush.Application.Contract.Interface
{
    public interface IMealRepository : IGeneraicRepository<Meal>
    {
        Task<List<GetMealDto>> GetAllMealAsync();
    }
}
