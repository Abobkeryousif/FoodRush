namespace FoodRush.Infrastructure.Implemention
{
    public class Unitofwork : IUnitofwork
    {
        private readonly ApplicationDbContaxt _contaxt;
        public Unitofwork(ApplicationDbContaxt contaxt)
        {
            _contaxt = contaxt;
            RestaurantRepository = new RestaurantRepository(_contaxt);
        }
        public IRestaurantRepository RestaurantRepository { get; }
    }
}
