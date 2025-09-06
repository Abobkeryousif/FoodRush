namespace FoodRush.Infrastructure.Implemention
{
    public class RestaurantRepository : GeneraicRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(ApplicationDbContaxt context) : base(context)
        {
        }
    }
}
