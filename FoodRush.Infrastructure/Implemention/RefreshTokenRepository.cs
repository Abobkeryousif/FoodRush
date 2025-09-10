
namespace FoodRush.Infrastructure.Implemention
{
    public class RefreshTokenRepository : MinimalRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationDbContaxt context) : base(context)
        {
        }
    }
}
