
namespace FoodRush.Domain.Entites.Order
{
    public class ShippingAddress
    {
        public ShippingAddress()
        {

        }
        public ShippingAddress(string firstName, string lastName, string city, string street, string zipCode)
        {
 
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Street = street;
            ZipCode = zipCode;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
    }
}
