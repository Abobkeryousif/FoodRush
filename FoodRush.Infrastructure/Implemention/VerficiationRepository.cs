
namespace FoodRush.Infrastructure.Implemention
{
    public class VerficiationRepository : MinimalRepository<Verficiation>, IVerficiationRepository
    {
        public VerficiationRepository(ApplicationDbContaxt context) : base(context)
        {
        }
    }
}
