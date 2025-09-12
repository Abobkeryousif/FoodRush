
namespace FoodRush.Application.DTOs
{
    public record OrderDto
    {
        public int deliveryId { get; set; }
        public string basketId { get; set; }
        public ShippingAddressDto ShippingAddress { get; set; }
    }

    public record ShippingAddressDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
    }
}
