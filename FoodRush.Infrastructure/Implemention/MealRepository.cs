
namespace FoodRush.Infrastructure.Implemention
{
    public class MealRepository : GeneraicRepository<Meal>, IMealRepository
    {
        private readonly ApplicationDbContaxt _contaxt;
        public MealRepository(ApplicationDbContaxt context) : base(context)
        {
            _contaxt = context;
        }
        public async Task<List<GetMealDto>> GetAllMealAsync()
        {
            var result = await _contaxt.Meals
                .Include(m => m.Restaurant)
                .Select(m => new GetMealDto
                {
                    Name = m.Name,
                    Description = m.Description,
                    Price = m.Price,
                    IsAvailable = m.IsAvailable,
                    Restaurant = new RestaurantDto
                    {
                        Name = m.Restaurant.Name,
                        Address = m.Restaurant.Address,
                        phoneNumber = m.Restaurant.phoneNumber,
                        Rating = m.Restaurant.Rating
                    }
                })
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

    }
}

