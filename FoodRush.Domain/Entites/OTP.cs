
namespace FoodRush.Domain.Entites
{
    public class OTP
    {
        public int Id { get; set; }
        public string otp { get; set; }
        public string UserEmail { get; set; }
        public DateTime ExpirationOn { get; set; }
        public bool IsUsed { get; set; } 
        public bool IsExpier => DateTime.Now > ExpirationOn;
    }
}
