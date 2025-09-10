
namespace FoodRush.Infrastructure.Implemention
{
    public class OtpRepository : MinimalRepository<OTP>, IOtpRepository
    {
        public OtpRepository(ApplicationDbContaxt context) : base(context)
        {
        }
    }
}
