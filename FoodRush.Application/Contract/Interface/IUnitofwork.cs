namespace FoodRush.Application.Contract.Interface
{
    public interface IUnitofwork
    {
        public IRestaurantRepository RestaurantRepository { get; }
    }
}
