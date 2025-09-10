
namespace FoodRush.Domain.Entites
{
    public class User 
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName => $"{firstName} {lastName}";
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public string Role { get; set; } = "USER";  

        [DataType(DataType.Date)]
        [Range(1980,2020)]
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password")]
        public string confirmPassword { get; set; }
        public string? ProfileImageUrl { get; set; }
        public List<RefreshToken> refreshTokens { get; set; } = new List<RefreshToken>();

    }
}
