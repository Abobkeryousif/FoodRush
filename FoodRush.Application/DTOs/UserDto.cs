
namespace FoodRush.Application.DTOs
{
    public record UserDto
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName => $"{firstName} {lastName}";
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }

        [Compare("Password")]
        public string confirmPassword { get; set; }
    }

    public record LoginUserDto
    {
        public string email { get; set; }  
        public string password { get; set; }
    }
    
    public record UpdateUserEmailDto
    {
        public string email { get; set; }
    }

    public record GetUserDto
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName => $"{firstName} {lastName}";
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
    }
    public record UpdateUserProfilePictureDto
    {
        public IFormFile File { get; set; } = default!;
    }


    public record UserOtpDto(string otp);
    
}
