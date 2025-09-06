namespace FoodRush.Application.DTOs
{
    public record RestaurantDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string phoneNumber { get; set; }
        public bool isOpen { get; set; } = true;
        public double? Rating { get; set; }
    }

    
}
