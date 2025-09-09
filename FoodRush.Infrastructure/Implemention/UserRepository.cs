

namespace FoodRush.Infrastructure.Implemention
{
    public class UserRepository : GeneraicRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContaxt context) : base(context)
        {
        }
    }
}
