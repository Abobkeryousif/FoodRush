
namespace FoodRush.Domain.Entites
{
    public class Verficiation
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public DateTime ExpierOn { get; set; }
        public bool IsUsed { get; set; }
        public bool IsExpier => DateTime.Now > ExpierOn;

        public TokenPerpouse tokenPerpouse { get; set; }
    }
}
