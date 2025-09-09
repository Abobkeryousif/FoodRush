namespace FoodRush.Application.Contract.Interface
{
    public interface IUnitofwork
    {
        public IRestaurantRepository RestaurantRepository { get; }
        public IMealRepository MealRepository { get; }
        public IUserRepository UserRepository { get; }
    }
}
