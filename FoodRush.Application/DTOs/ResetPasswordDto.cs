
namespace FoodRush.Application.DTOs
{
    public record ResetPasswordDto
    {
        public string email { get; set; }
        public string token { get; set; }
        public string password { get; set; }

        [Compare("password")]
        public string confirmPassword { get; set; }
    }
}
