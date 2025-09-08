
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

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password")]
        public string confirmPassword { get; set; }


    }
}
